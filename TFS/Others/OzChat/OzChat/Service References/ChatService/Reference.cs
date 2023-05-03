﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OzChat.ChatService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConversationItem", Namespace="http://schemas.datacontract.org/2004/07/OzChatServer")]
    [System.SerializableAttribute()]
    public partial class ConversationItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FromPersonField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextBrushField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeRecievedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeSentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToPersonField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FromPerson {
            get {
                return this.FromPersonField;
            }
            set {
                if ((object.ReferenceEquals(this.FromPersonField, value) != true)) {
                    this.FromPersonField = value;
                    this.RaisePropertyChanged("FromPerson");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TextBrush {
            get {
                return this.TextBrushField;
            }
            set {
                if ((object.ReferenceEquals(this.TextBrushField, value) != true)) {
                    this.TextBrushField = value;
                    this.RaisePropertyChanged("TextBrush");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeRecieved {
            get {
                return this.TimeRecievedField;
            }
            set {
                if ((this.TimeRecievedField.Equals(value) != true)) {
                    this.TimeRecievedField = value;
                    this.RaisePropertyChanged("TimeRecieved");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeSent {
            get {
                return this.TimeSentField;
            }
            set {
                if ((this.TimeSentField.Equals(value) != true)) {
                    this.TimeSentField = value;
                    this.RaisePropertyChanged("TimeSent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToPerson {
            get {
                return this.ToPersonField;
            }
            set {
                if ((object.ReferenceEquals(this.ToPersonField, value) != true)) {
                    this.ToPersonField = value;
                    this.RaisePropertyChanged("ToPerson");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChatService.IChatService")]
    public interface IChatService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/PostConversationItem", ReplyAction="http://tempuri.org/IChatService/PostConversationItemResponse")]
        void PostConversationItem(OzChat.ChatService.ConversationItem item);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/PostConversationItem", ReplyAction="http://tempuri.org/IChatService/PostConversationItemResponse")]
        System.Threading.Tasks.Task PostConversationItemAsync(OzChat.ChatService.ConversationItem item);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetNextMessage", ReplyAction="http://tempuri.org/IChatService/GetNextMessageResponse")]
        OzChat.ChatService.GetNextMessageResponse GetNextMessage(OzChat.ChatService.GetNextMessageRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetNextMessage", ReplyAction="http://tempuri.org/IChatService/GetNextMessageResponse")]
        System.Threading.Tasks.Task<OzChat.ChatService.GetNextMessageResponse> GetNextMessageAsync(OzChat.ChatService.GetNextMessageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetAllUsers", ReplyAction="http://tempuri.org/IChatService/GetAllUsersResponse")]
        System.Collections.Generic.Dictionary<string, string> GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/GetAllUsers", ReplyAction="http://tempuri.org/IChatService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/RegisterUser", ReplyAction="http://tempuri.org/IChatService/RegisterUserResponse")]
        void RegisterUser(string name, string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/RegisterUser", ReplyAction="http://tempuri.org/IChatService/RegisterUserResponse")]
        System.Threading.Tasks.Task RegisterUserAsync(string name, string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/UnRegister", ReplyAction="http://tempuri.org/IChatService/UnRegisterResponse")]
        void UnRegister(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/UnRegister", ReplyAction="http://tempuri.org/IChatService/UnRegisterResponse")]
        System.Threading.Tasks.Task UnRegisterAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/AcknowledgeRequest", ReplyAction="http://tempuri.org/IChatService/AcknowledgeRequestResponse")]
        void AcknowledgeRequest(string requestingUser, string acknowledgingUser, bool isAccepted);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatService/AcknowledgeRequest", ReplyAction="http://tempuri.org/IChatService/AcknowledgeRequestResponse")]
        System.Threading.Tasks.Task AcknowledgeRequestAsync(string requestingUser, string acknowledgingUser, bool isAccepted);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetNextMessage", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetNextMessageRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string userName;
        
        public GetNextMessageRequest() {
        }
        
        public GetNextMessageRequest(string userName) {
            this.userName = userName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetNextMessageResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetNextMessageResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public OzChat.ChatService.ConversationItem GetNextMessageResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public bool hasNewUsers;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string requestConversation;
        
        public GetNextMessageResponse() {
        }
        
        public GetNextMessageResponse(OzChat.ChatService.ConversationItem GetNextMessageResult, bool hasNewUsers, string requestConversation) {
            this.GetNextMessageResult = GetNextMessageResult;
            this.hasNewUsers = hasNewUsers;
            this.requestConversation = requestConversation;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatServiceChannel : OzChat.ChatService.IChatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatServiceClient : System.ServiceModel.ClientBase<OzChat.ChatService.IChatService>, OzChat.ChatService.IChatService {
        
        public ChatServiceClient() {
        }
        
        public ChatServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ChatServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChatServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void PostConversationItem(OzChat.ChatService.ConversationItem item) {
            base.Channel.PostConversationItem(item);
        }
        
        public System.Threading.Tasks.Task PostConversationItemAsync(OzChat.ChatService.ConversationItem item) {
            return base.Channel.PostConversationItemAsync(item);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        OzChat.ChatService.GetNextMessageResponse OzChat.ChatService.IChatService.GetNextMessage(OzChat.ChatService.GetNextMessageRequest request) {
            return base.Channel.GetNextMessage(request);
        }
        
        public OzChat.ChatService.ConversationItem GetNextMessage(string userName, out bool hasNewUsers, out string requestConversation) {
            OzChat.ChatService.GetNextMessageRequest inValue = new OzChat.ChatService.GetNextMessageRequest();
            inValue.userName = userName;
            OzChat.ChatService.GetNextMessageResponse retVal = ((OzChat.ChatService.IChatService)(this)).GetNextMessage(inValue);
            hasNewUsers = retVal.hasNewUsers;
            requestConversation = retVal.requestConversation;
            return retVal.GetNextMessageResult;
        }
        
        public System.Threading.Tasks.Task<OzChat.ChatService.GetNextMessageResponse> GetNextMessageAsync(OzChat.ChatService.GetNextMessageRequest request) {
            return base.Channel.GetNextMessageAsync(request);
        }
        
        public System.Collections.Generic.Dictionary<string, string> GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, string>> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public void RegisterUser(string name, string userName) {
            base.Channel.RegisterUser(name, userName);
        }
        
        public System.Threading.Tasks.Task RegisterUserAsync(string name, string userName) {
            return base.Channel.RegisterUserAsync(name, userName);
        }
        
        public void UnRegister(string userName) {
            base.Channel.UnRegister(userName);
        }
        
        public System.Threading.Tasks.Task UnRegisterAsync(string userName) {
            return base.Channel.UnRegisterAsync(userName);
        }
        
        public void AcknowledgeRequest(string requestingUser, string acknowledgingUser, bool isAccepted) {
            base.Channel.AcknowledgeRequest(requestingUser, acknowledgingUser, isAccepted);
        }
        
        public System.Threading.Tasks.Task AcknowledgeRequestAsync(string requestingUser, string acknowledgingUser, bool isAccepted) {
            return base.Channel.AcknowledgeRequestAsync(requestingUser, acknowledgingUser, isAccepted);
        }
    }
}
