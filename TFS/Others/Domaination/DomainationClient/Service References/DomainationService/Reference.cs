namespace DomainationClient.DomainationService {
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DomainationService.IDomainationService")]
    public interface IDomainationService {
        [System.ServiceModel.OperationContractAttribute(Action="http:
        bool Test();
        [System.ServiceModel.OperationContractAttribute(Action="http:
        System.Threading.Tasks.Task<bool> TestAsync();
        [System.ServiceModel.OperationContractAttribute(Action="http:
        DomainationClient.DomainationService.LoginResponse Login(DomainationClient.DomainationService.LoginRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="http:
        System.Threading.Tasks.Task<DomainationClient.DomainationService.LoginResponse> LoginAsync(DomainationClient.DomainationService.LoginRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="http:
        [return: System.ServiceModel.MessageParameterAttribute(Name="userAlreadyExists")]
        bool CreateUser(DomainationData.Entities.User user, string password);
        [System.ServiceModel.OperationContractAttribute(Action="http:
        [return: System.ServiceModel.MessageParameterAttribute(Name="userAlreadyExists")]
        System.Threading.Tasks.Task<bool> CreateUserAsync(DomainationData.Entities.User user, string password);
    }
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Login", WrapperNamespace="http:
    public partial class LoginRequest {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http:
        public string userName;
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http:
        public string password;
        public LoginRequest() {
        }
        public LoginRequest(string userName, string password) {
            this.userName = userName;
            this.password = password;
        }
    }
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="LoginResponse", WrapperNamespace="http:
    public partial class LoginResponse {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http:
        public DomainationData.Entities.User LoginResult;
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http:
        public bool newUserRequired;
        public LoginResponse() {
        }
        public LoginResponse(DomainationData.Entities.User LoginResult, bool newUserRequired) {
            this.LoginResult = LoginResult;
            this.newUserRequired = newUserRequired;
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDomainationServiceChannel : DomainationClient.DomainationService.IDomainationService, System.ServiceModel.IClientChannel {
    }
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DomainationServiceClient : System.ServiceModel.ClientBase<DomainationClient.DomainationService.IDomainationService>, DomainationClient.DomainationService.IDomainationService {
        public DomainationServiceClient() {
        }
        public DomainationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        public DomainationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        public DomainationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        public DomainationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        public bool Test() {
            return base.Channel.Test();
        }
        public System.Threading.Tasks.Task<bool> TestAsync() {
            return base.Channel.TestAsync();
        }
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        DomainationClient.DomainationService.LoginResponse DomainationClient.DomainationService.IDomainationService.Login(DomainationClient.DomainationService.LoginRequest request) {
            return base.Channel.Login(request);
        }
        public DomainationData.Entities.User Login(string userName, string password, out bool newUserRequired) {
            DomainationClient.DomainationService.LoginRequest inValue = new DomainationClient.DomainationService.LoginRequest();
            inValue.userName = userName;
            inValue.password = password;
            DomainationClient.DomainationService.LoginResponse retVal = ((DomainationClient.DomainationService.IDomainationService)(this)).Login(inValue);
            newUserRequired = retVal.newUserRequired;
            return retVal.LoginResult;
        }
        public System.Threading.Tasks.Task<DomainationClient.DomainationService.LoginResponse> LoginAsync(DomainationClient.DomainationService.LoginRequest request) {
            return base.Channel.LoginAsync(request);
        }
        public bool CreateUser(DomainationData.Entities.User user, string password) {
            return base.Channel.CreateUser(user, password);
        }
        public System.Threading.Tasks.Task<bool> CreateUserAsync(DomainationData.Entities.User user, string password) {
            return base.Channel.CreateUserAsync(user, password);
        }
    }
}
