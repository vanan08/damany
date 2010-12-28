using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;

namespace IdQueryService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IdQueryProvider" in both code and config file together.
    public class IdQueryProvider : IIdQueryProvider
    {
        private RBSPAdapter_COM.RSBPAdapterCOMObjClass _q;

        public string QueryIdCard(string queryType, string queryString)
        {
            _q = null;
            try
            {
                Console.WriteLine("----------------\r\n" + queryString + queryType + System.Threading.Thread.CurrentThread.GetApartmentState());
                //if (_q == null)
                {
                    _q = new RBSPAdapter_COM.RSBPAdapterCOMObjClass();
                }
                _q.queryCondition = queryString;
                _q.queryType = queryType;
                var result = _q.queryCondition;
                Console.WriteLine(result + "\r\n" + "---------------");
                return result;
            }
            finally
            {
                if (_q != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_q);
                }
            }
        }
    }

    public class STAOperationBehaviorAttribute : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription,
          System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription,
          System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
            // If this is applied on the client, well, it just doesn't make sense.
            // Don't throw in case this attribute was applied on the contract
            // instead of the implementation.
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription,
          System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            // Change the IOperationInvoker for this operation.
            dispatchOperation.Invoker = new STAOperationInvoker(dispatchOperation.Invoker);
        }

        public void Validate(OperationDescription operationDescription)
        {
            if (operationDescription.SyncMethod == null)
            {
                throw new InvalidOperationException("The STAOperationBehaviorAttribute " +
                    "only works for synchronous method invocations.");
            }
        }
    }


    public class STAOperationInvoker : IOperationInvoker
    {
        IOperationInvoker _innerInvoker;
        public STAOperationInvoker(IOperationInvoker invoker)
        {
            _innerInvoker = invoker;
        }

        public object[] AllocateInputs()
        {
            return _innerInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            // Create a new, STA thread
            object[] staOutputs = null;
            object retval = null;
            Thread thread = new Thread(
                delegate()
                {
                    retval = _innerInvoker.Invoke(instance, inputs, out staOutputs);
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            outputs = staOutputs;
            return retval;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs,
          AsyncCallback callback, object state)
        {
            // We don't handle async...
            throw new NotImplementedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            // We don't handle async...
            throw new NotImplementedException();
        }

        public bool IsSynchronous
        {
            get { return true; }
        }
    }

}
