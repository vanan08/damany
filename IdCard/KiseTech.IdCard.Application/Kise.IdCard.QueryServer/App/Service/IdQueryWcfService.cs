﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Kise.IdCard.Messaging;
using Kise.IdCard.Server;
using Helper = Kise.IdCard.Server.Helper;

namespace Kise.IdCard.QueryServer.UI.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdQueryWcfService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class IdQueryWcfService : IIdQueryWcfService
    {
        private TcpClientChannel _channel;
        private IdQueryServiceContract.IIdQueryProvider _provider;
        public static ILog _logger;

        private static NLog.Logger _fileLogger = NLog.LogManager.GetCurrentClassLogger();

        public IdQueryWcfService()
        {
            _provider = CreateIdQueryProvider();
        }

        public string QueryId(string queryString)
        {
            LogEnter(queryString);

            var replyResult = DoQuery(queryString);
            var replyString = FormatReplyString(replyResult, queryString);

            LogExit(replyString);

            return replyString;
        }

        private QueryResult DoQuery(string queryString)
        {
            var replyResult = new QueryResult();

            try
            {
                var idQueryString = string.Format("sfzh='{0}'", queryString);
                var queryResult = _provider.QueryIdCard(idQueryString);
                var normalIdInfo = Helper.Parse(queryResult.NormalResult);
                var suspectIdInfo = Helper.Parse(queryResult.SuspectResult);

                replyResult.ErrorCode = 0;
                if (normalIdInfo.Length > 0)
                {
                    replyResult.IdInfo = normalIdInfo[0];
                }

                replyResult.IsSuspect = suspectIdInfo.Length > 0;
            }
            catch (System.Xml.XmlException)
            {
                replyResult.ErrorCode = 1;
            }

            return replyResult;
        }

        private static void LogExit(string replyString)
        {
            var entry = new LogEntry();
            entry.Description = "发送应答：" + replyString;
            _logger.Log(entry);
        }

        private static void LogEnter(string queryString)
        {
            var ep = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as
                     RemoteEndpointMessageProperty;
            var entry = new LogEntry();
            entry.Sender = ep.Address;


            entry.Description = "收到查询: " + queryString;
            _logger.Log(entry);

            entry = new LogEntry();
            entry.Description = "查询数据库...";
            _logger.Log(entry);
        }

        public IdQueryResult QueryByIdNumber(string idNumber)
        {
            LogEnter(idNumber);
            var qr = DoQuery(idNumber);

            var result = new IdQueryResult();
            result.ErrorCode = qr.ErrorCode;
            if (qr.ErrorCode == 0 && qr.IdInfo != null)
            {
                var info = new IdCardInfo();

                info.Address = qr.IdInfo.Address;
                info.BirthDay = qr.IdInfo.BornDate;
                if (qr.IdInfo.PhotoData != null)
                {
                    info.Icon = Convert.ToBase64String(qr.IdInfo.PhotoData);
                }
                info.IdNumber = qr.IdInfo.IdCardNo;
                info.IsWanted = qr.IsSuspect;
                info.IssueDate = qr.IdInfo.ValidateFrom;
                info.IssueDepartment = qr.IdInfo.GrantDept;
                info.Minority = qr.IdInfo.MinorityCode;
                info.Name = qr.IdInfo.Name;
                info.Sex = qr.IdInfo.SexCode;
                info.ValidateUntil = qr.IdInfo.ValidateUntil;

                var msg =
                    string.Format(
                        "name: {0}, sex:{1}, minority:{2}, birthday:{3}, add:{4}, issuedate:{5}, idnumber:{6}",
                        info.Name, info.Sex, info.Minority, info.BirthDay, info.Address, info.IssueDate,
                        info.IdNumber);
                _fileLogger.Trace(msg);

                result.Info = info;
            }
            
            LogExit(idNumber);
            return result;
        }


        private IdQueryServiceContract.IIdQueryProvider CreateIdQueryProvider()
        {
            //
            // TODO: Add code to start application here
            //
            if (_channel == null)
            {
                _channel = new TcpClientChannel();
                ChannelServices.RegisterChannel(_channel, false);
            }

            var url = string.Format("tcp://localhost:{0}/IdQueryService", UI.Properties.Settings.Default.IdQueryServerTcpPortNo);

            var provider = (IdQueryServiceContract.IIdQueryProvider)Activator.GetObject
            (
                typeof(IdQueryServiceContract.IIdQueryProvider),
                url
            );

            return provider;
        }


        private string FormatReplyString(QueryResult queryResult, string idCardNo)
        {
            var strings = new List<string>();
            strings.Add(idCardNo);
            strings.Add(queryResult.ErrorCode.ToString());

            if (queryResult.ErrorCode == 0)
            {
                strings.Add(string.IsNullOrEmpty(queryResult.IdInfo.Name) ? Messaging.Constants.EmptyString : queryResult.IdInfo.Name);
                strings.Add(queryResult.IdInfo.SexCode.HasValue ? queryResult.IdInfo.SexCode.Value.ToString() : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IdInfo.MinorityCode.HasValue ? queryResult.IdInfo.MinorityCode.ToString() : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IdInfo.BornDate.HasValue ? queryResult.IdInfo.BornDate.Value.ToString(Messaging.Constants.BirthDayFormatString) : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IsSuspect ? "1" : "0");
            }

            var result = string.Join(Messaging.Constants.SplitterChar.ToString(), strings);
            return result;
        }
    }
}