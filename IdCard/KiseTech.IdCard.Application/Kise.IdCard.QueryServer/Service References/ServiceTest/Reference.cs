﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kise.IdCard.QueryServer.UI.ServiceTest {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceTest.IIdQueryProvider")]
    public interface IIdQueryProvider {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIdQueryProvider/QueryIdCard", ReplyAction="http://tempuri.org/IIdQueryProvider/QueryIdCardResponse")]
        string QueryIdCard(string queryType, string queryString);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIdQueryProviderChannel : Kise.IdCard.QueryServer.UI.ServiceTest.IIdQueryProvider, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IdQueryProviderClient : System.ServiceModel.ClientBase<Kise.IdCard.QueryServer.UI.ServiceTest.IIdQueryProvider>, Kise.IdCard.QueryServer.UI.ServiceTest.IIdQueryProvider {
        
        public IdQueryProviderClient() {
        }
        
        public IdQueryProviderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IdQueryProviderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IdQueryProviderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IdQueryProviderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string QueryIdCard(string queryType, string queryString) {
            return base.Channel.QueryIdCard(queryType, queryString);
        }
    }
}
