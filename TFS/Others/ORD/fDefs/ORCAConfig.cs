﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 
namespace SNC.OptiRamp.Services.ORCA {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class ORCAConfiguration {
        
        private ORCAConfigurationGeneral generalField;
        
        private ORCAConfigurationControlBlock[] controlBlocksField;
        
        private ORCAConfigurationAdvancedParameters advancedParametersField;
        
        /// <remarks/>
        public ORCAConfigurationGeneral General {
            get {
                return this.generalField;
            }
            set {
                this.generalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ControlBlock", IsNullable=false)]
        public ORCAConfigurationControlBlock[] ControlBlocks {
            get {
                return this.controlBlocksField;
            }
            set {
                this.controlBlocksField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParameters AdvancedParameters {
            get {
                return this.advancedParametersField;
            }
            set {
                this.advancedParametersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationGeneral {
        
        private ORCAConfigurationGeneralServer serverField;
        
        private ORCAConfigurationGeneralControlModule controlModuleField;
        
        private string nameField;
        
        /// <remarks/>
        public ORCAConfigurationGeneralServer Server {
            get {
                return this.serverField;
            }
            set {
                this.serverField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationGeneralControlModule ControlModule {
            get {
                return this.controlModuleField;
            }
            set {
                this.controlModuleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationGeneralServer {
        
        private string addressField;
        
        private string idField;
        
        private ushort scanRateField;
        
        private bool scanRateFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort ScanRate {
            get {
                return this.scanRateField;
            }
            set {
                this.scanRateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ScanRateSpecified {
            get {
                return this.scanRateFieldSpecified;
            }
            set {
                this.scanRateFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationGeneralControlModule {
        
        private string nameField;
        
        private string oPCITemRecordingIntervalField;
        
        private string samplesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OPCITemRecordingInterval {
            get {
                return this.oPCITemRecordingIntervalField;
            }
            set {
                this.oPCITemRecordingIntervalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Samples {
            get {
                return this.samplesField;
            }
            set {
                this.samplesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationControlBlock {
        
        private string nameField;
        
        private string euField;
        
        private string descriptionField;
        
        private string lineThicknessField;
        
        private string lineColorField;
        
        private string axisMinScaleField;
        
        private string axisMaxScaleField;
        
        private string autoScaleField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EU {
            get {
                return this.euField;
            }
            set {
                this.euField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LineThickness {
            get {
                return this.lineThicknessField;
            }
            set {
                this.lineThicknessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LineColor {
            get {
                return this.lineColorField;
            }
            set {
                this.lineColorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AxisMinScale {
            get {
                return this.axisMinScaleField;
            }
            set {
                this.axisMinScaleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AxisMaxScale {
            get {
                return this.axisMaxScaleField;
            }
            set {
                this.axisMaxScaleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AutoScale {
            get {
                return this.autoScaleField;
            }
            set {
                this.autoScaleField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParameters {
        
        private ORCAConfigurationAdvancedParametersCommunication communicationField;
        
        private ORCAConfigurationAdvancedParametersOPCItems oPCItemsField;
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersCommunication Communication {
            get {
                return this.communicationField;
            }
            set {
                this.communicationField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItems OPCItems {
            get {
                return this.oPCItemsField;
            }
            set {
                this.oPCItemsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersCommunication {
        
        private byte timeOutField;
        
        private bool timeOutFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte TimeOut {
            get {
                return this.timeOutField;
            }
            set {
                this.timeOutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimeOutSpecified {
            get {
                return this.timeOutFieldSpecified;
            }
            set {
                this.timeOutFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItems {
        
        private ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumber currentTableNumberField;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeSecAfterCE timeSecAfterCEField;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET1 timeDTLastCET1Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET1 timeMSLastCET1Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET2 timeDTLastCET2Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET2 timeMSLastCET2Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET3 timeDTLastCET3Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET3 timeMSLastCET3Field;
        
        private ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRequest currentTableNumberRequestField;
        
        private ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRecording currentTableNumberRecordingField;
        
        private ORCAConfigurationAdvancedParametersOPCItemsStringData stringDataField;
        
        private ORCAConfigurationAdvancedParametersOPCItemsStringDataArrayIndex stringDataArrayIndexField;
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumber CurrentTableNumber {
            get {
                return this.currentTableNumberField;
            }
            set {
                this.currentTableNumberField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeSecAfterCE TimeSecAfterCE {
            get {
                return this.timeSecAfterCEField;
            }
            set {
                this.timeSecAfterCEField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET1 TimeDTLastCET1 {
            get {
                return this.timeDTLastCET1Field;
            }
            set {
                this.timeDTLastCET1Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET1 TimeMSLastCET1 {
            get {
                return this.timeMSLastCET1Field;
            }
            set {
                this.timeMSLastCET1Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET2 TimeDTLastCET2 {
            get {
                return this.timeDTLastCET2Field;
            }
            set {
                this.timeDTLastCET2Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET2 TimeMSLastCET2 {
            get {
                return this.timeMSLastCET2Field;
            }
            set {
                this.timeMSLastCET2Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET3 TimeDTLastCET3 {
            get {
                return this.timeDTLastCET3Field;
            }
            set {
                this.timeDTLastCET3Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET3 TimeMSLastCET3 {
            get {
                return this.timeMSLastCET3Field;
            }
            set {
                this.timeMSLastCET3Field = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRequest CurrentTableNumberRequest {
            get {
                return this.currentTableNumberRequestField;
            }
            set {
                this.currentTableNumberRequestField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRecording CurrentTableNumberRecording {
            get {
                return this.currentTableNumberRecordingField;
            }
            set {
                this.currentTableNumberRecordingField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsStringData StringData {
            get {
                return this.stringDataField;
            }
            set {
                this.stringDataField = value;
            }
        }
        
        /// <remarks/>
        public ORCAConfigurationAdvancedParametersOPCItemsStringDataArrayIndex StringDataArrayIndex {
            get {
                return this.stringDataArrayIndexField;
            }
            set {
                this.stringDataArrayIndexField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumber {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeSecAfterCE {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET1 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET1 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET2 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET2 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeDTLastCET3 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsTimeMSLastCET3 {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRequest {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsCurrentTableNumberRecording {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsStringData {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ORCAConfigurationAdvancedParametersOPCItemsStringDataArrayIndex {
        
        private string itemField;
        
        private string accessField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Access {
            get {
                return this.accessField;
            }
            set {
                this.accessField = value;
            }
        }
    }
}
