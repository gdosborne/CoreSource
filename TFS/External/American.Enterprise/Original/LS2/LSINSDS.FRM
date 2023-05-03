VERSION 2.00
Begin Form frmLSINSD 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Life Savings II - Additional Insureds and Riders"
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Height          =   7395
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6915
   ScaleWidth      =   9585
   Top             =   0
   Width           =   9705
   Begin SSCommand cmdFuture 
      BevelWidth      =   4
      Caption         =   "&FUTURE Changes"
      Height          =   735
      Left            =   4920
      TabIndex        =   131
      Top             =   6000
      Width           =   1815
   End
   Begin SSCommand cmdRefreshInsureds 
      BevelWidth      =   4
      Caption         =   "&REFRESH "
      Height          =   735
      Left            =   7080
      TabIndex        =   132
      Top             =   6000
      Width           =   1695
   End
   Begin SSCommand cmdPremCalc 
      BevelWidth      =   4
      Caption         =   "&PREMIUM CALC"
      Height          =   735
      Left            =   2760
      TabIndex        =   130
      Top             =   6000
      Width           =   1815
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      Height          =   735
      Left            =   480
      TabIndex        =   129
      Top             =   6000
      Width           =   1935
   End
   Begin Frame fraSpouseRider 
      Caption         =   "Insured Spouse Rider "
      ClipControls    =   0   'False
      Height          =   1575
      Left            =   2880
      TabIndex        =   6
      Top             =   120
      Width           =   6495
      Begin ComboBox cboSpouseYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   3600
         Style           =   2  'Dropdown List
         TabIndex        =   18
         Top             =   1200
         Visible         =   0   'False
         Width           =   735
      End
      Begin MaskEdBox txtSpouseYears 
         Height          =   285
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   17
         Top             =   1200
         Visible         =   0   'False
         Width           =   375
      End
      Begin ComboBox cboSpouseCOLA 
         Enabled         =   0   'False
         Height          =   300
         Left            =   5520
         Style           =   2  'Dropdown List
         TabIndex        =   20
         TabStop         =   0   'False
         Top             =   1200
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboSpouseSubStd 
         Height          =   300
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   19
         Top             =   1200
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboSpouseFace 
         Height          =   300
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   16
         Top             =   1200
         Visible         =   0   'False
         Width           =   1215
      End
      Begin MaskEdBox txtSpouseAge 
         Height          =   285
         Left            =   120
         PromptChar      =   "_"
         TabIndex        =   14
         Top             =   1200
         Visible         =   0   'False
         Width           =   375
      End
      Begin ComboBox cboSpousePlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   600
         Style           =   2  'Dropdown List
         TabIndex        =   15
         Top             =   1200
         Visible         =   0   'False
         Width           =   1215
      End
      Begin CheckBox chkSpouse 
         Caption         =   "Show?"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   210
         Left            =   120
         TabIndex        =   7
         Top             =   360
         Width           =   855
      End
      Begin Label lblSpouseRiderYears 
         Alignment       =   2  'Center
         Caption         =   "For ?? Years or To Age ??"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   3240
         TabIndex        =   11
         Top             =   720
         Width           =   1095
      End
      Begin Label lblAddInsdSpouseCOLA 
         Alignment       =   2  'Center
         Caption         =   "COLA percent"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   5640
         TabIndex        =   13
         Top             =   720
         Width           =   615
      End
      Begin Label lblAddInsdSpouseSubStd 
         Alignment       =   2  'Center
         Caption         =   "Table Rating (Substandard)"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   4440
         TabIndex        =   12
         Top             =   720
         Width           =   1095
      End
      Begin Label lblAddInsdSpouseFace 
         Alignment       =   2  'Center
         Caption         =   "Death Benefit Amount"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   1920
         TabIndex        =   10
         Top             =   720
         Width           =   1095
      End
      Begin Label lblAddInsdSpouseAge 
         Alignment       =   2  'Center
         Caption         =   "Issue Age"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   8
         Top             =   720
         Width           =   495
      End
      Begin Label lblAddInsdSpousePlan 
         Caption         =   "Plan Type"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   840
         TabIndex        =   9
         Top             =   840
         Width           =   855
      End
   End
   Begin Frame fraAddInsdChild 
      Caption         =   "Insured Child(ren) Rider"
      ClipControls    =   0   'False
      Height          =   1575
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   2655
      Begin CheckBox chkChild 
         Caption         =   "Show?"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   195
         Left            =   120
         TabIndex        =   1
         Top             =   360
         Width           =   855
      End
      Begin ComboBox cboChildFace 
         Height          =   300
         Left            =   1320
         Style           =   2  'Dropdown List
         TabIndex        =   5
         Top             =   1200
         Visible         =   0   'False
         Width           =   1215
      End
      Begin MaskEdBox txtChildAge 
         Height          =   285
         Left            =   480
         PromptChar      =   "_"
         TabIndex        =   4
         Top             =   1200
         Visible         =   0   'False
         Width           =   375
      End
      Begin Label lblAddInsdChildFace 
         Alignment       =   2  'Center
         Caption         =   "Death Benefit Amount"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   1320
         TabIndex        =   3
         Top             =   720
         Width           =   1215
      End
      Begin Label lblAddInsdChildAge 
         Alignment       =   2  'Center
         Caption         =   "Age Youngest Child"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   2
         Top             =   720
         Width           =   1095
      End
   End
   Begin Frame fraAddInsureds 
      Caption         =   "Additional Insureds"
      ClipControls    =   0   'False
      Height          =   3975
      Left            =   120
      TabIndex        =   21
      Top             =   1800
      Width           =   9255
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   8
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   125
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   7
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   113
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   6
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   103
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   5
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   92
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   4
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   81
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   3
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   70
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   2
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   59
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   1
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   48
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         Height          =   285
         Index           =   0
         Left            =   6720
         PromptChar      =   "_"
         TabIndex        =   37
         Top             =   720
         Visible         =   0   'False
         Width           =   375
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   8
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   126
         Top             =   3600
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   7
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   114
         Top             =   3240
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   6
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   104
         Top             =   2880
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   93
         Top             =   2520
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   82
         Top             =   2160
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   71
         Top             =   1800
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   60
         Top             =   1440
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   49
         Top             =   1080
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdStartYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   7080
         Style           =   2  'Dropdown List
         TabIndex        =   38
         Top             =   720
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   8
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   128
         Top             =   3600
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   7
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   116
         Top             =   3240
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   6
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   106
         Top             =   2880
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   95
         Top             =   2520
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   84
         Top             =   2160
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   73
         Top             =   1800
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   62
         Top             =   1440
         Visible         =   0   'False
         Width           =   735
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   51
         Top             =   1080
         Visible         =   0   'False
         Width           =   735
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   8
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   127
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   7
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   115
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   6
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   105
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   5
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   94
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   4
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   83
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   3
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   72
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   2
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   61
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   1
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   50
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   2
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   56
         Top             =   1440
         Visible         =   0   'False
         Width           =   1095
      End
      Begin ComboBox cboAddInsdEndYearsAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   40
         Top             =   720
         Visible         =   0   'False
         Width           =   735
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         Height          =   285
         Index           =   0
         Left            =   8040
         PromptChar      =   "_"
         TabIndex        =   39
         Top             =   720
         Visible         =   0   'False
         Width           =   375
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   33
         Top             =   720
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   8
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   121
         Top             =   3600
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   7
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   110
         Top             =   3240
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   6
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   99
         Top             =   2880
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   88
         Top             =   2520
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   77
         Top             =   2160
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   66
         Top             =   1800
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   55
         Top             =   1440
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdPlan 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   1920
         Style           =   2  'Dropdown List
         TabIndex        =   44
         Top             =   1080
         Visible         =   0   'False
         Width           =   1215
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   8
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   124
         Top             =   3600
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   7
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   112
         TabStop         =   0   'False
         Top             =   3240
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   6
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   102
         TabStop         =   0   'False
         Top             =   2880
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   5
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   91
         TabStop         =   0   'False
         Top             =   2520
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   4
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   80
         TabStop         =   0   'False
         Top             =   2160
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   3
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   69
         TabStop         =   0   'False
         Top             =   1800
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   2
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   58
         TabStop         =   0   'False
         Top             =   1440
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   1
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   47
         TabStop         =   0   'False
         Top             =   1080
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdCOLA 
         Enabled         =   0   'False
         Height          =   300
         Index           =   0
         Left            =   5640
         Style           =   2  'Dropdown List
         TabIndex        =   36
         TabStop         =   0   'False
         Top             =   720
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   8
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   123
         Top             =   3600
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   7
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   117
         Top             =   3240
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   6
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   101
         Top             =   2880
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   5
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   90
         Top             =   2520
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   4
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   79
         Top             =   2160
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   3
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   68
         Top             =   1800
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   2
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   57
         Top             =   1440
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   1
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   46
         Top             =   1080
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         Height          =   300
         Index           =   0
         Left            =   4560
         Style           =   2  'Dropdown List
         TabIndex        =   35
         Top             =   720
         Visible         =   0   'False
         Width           =   855
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   8
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   122
         Top             =   3600
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   7
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   111
         Top             =   3240
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   6
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   100
         Top             =   2880
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   5
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   89
         Top             =   2520
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   4
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   78
         Top             =   2160
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   3
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   67
         Top             =   1800
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   1
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   45
         Top             =   1080
         Visible         =   0   'False
         Width           =   1095
      End
      Begin MaskEdBox txtAddInsdFace 
         Height          =   285
         Index           =   0
         Left            =   3240
         PromptChar      =   "_"
         TabIndex        =   34
         Top             =   720
         Visible         =   0   'False
         Width           =   1095
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   8
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   120
         Text            =   " "
         Top             =   3600
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   7
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   109
         Text            =   " "
         Top             =   3240
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   6
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   98
         Text            =   " "
         Top             =   2880
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   5
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   87
         Text            =   " "
         Top             =   2520
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   4
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   76
         Text            =   " "
         Top             =   2160
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   3
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   65
         Text            =   " "
         Top             =   1800
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   2
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   54
         Text            =   " "
         Top             =   1440
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   1
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   43
         Text            =   " "
         Top             =   1080
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         Height          =   285
         Index           =   0
         Left            =   1440
         MaxLength       =   1
         TabIndex        =   32
         Text            =   " "
         Top             =   720
         Visible         =   0   'False
         Width           =   255
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   315
         Index           =   8
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   119
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   315
         Index           =   7
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   108
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   6
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   97
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   5
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   86
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   4
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   75
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   3
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   64
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   2
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   53
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   1
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   42
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         Height          =   285
         Index           =   0
         Left            =   840
         PromptChar      =   "_"
         TabIndex        =   31
         Top             =   720
         Visible         =   0   'False
         Width           =   375
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(1)"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   30
         Top             =   720
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(5)"
         Height          =   255
         Index           =   4
         Left            =   120
         TabIndex        =   74
         Top             =   2160
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(4)"
         Height          =   255
         Index           =   3
         Left            =   120
         TabIndex        =   63
         Top             =   1800
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(3)"
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   52
         Top             =   1440
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(2)"
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   41
         Top             =   1080
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(6)"
         Height          =   255
         Index           =   5
         Left            =   120
         TabIndex        =   85
         Top             =   2520
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(7)"
         Height          =   255
         Index           =   6
         Left            =   120
         TabIndex        =   96
         Top             =   2880
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(8)"
         Height          =   255
         Index           =   7
         Left            =   120
         TabIndex        =   107
         Top             =   3240
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(9)"
         Height          =   255
         Index           =   8
         Left            =   120
         TabIndex        =   118
         Top             =   3600
         Width           =   615
      End
      Begin Label lblInsuredTermRider 
         Alignment       =   2  'Center
         Caption         =   "Remove at Year or Age"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   2
         Left            =   8040
         TabIndex        =   134
         Top             =   360
         Width           =   1095
      End
      Begin Label lblInsuredTermRider 
         Alignment       =   2  'Center
         Caption         =   "Add at Year or Age"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   6720
         TabIndex        =   133
         Top             =   360
         Width           =   1095
      End
      Begin Label lblAddInsd 
         Alignment       =   2  'Center
         Caption         =   "Show?"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   120
         TabIndex        =   22
         Top             =   360
         Width           =   615
      End
      Begin Label lblInsuredTermRider 
         Alignment       =   2  'Center
         Caption         =   "--- Add-on and Removal Years ---"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   6600
         TabIndex        =   29
         Top             =   120
         Width           =   2535
      End
      Begin Label lblAddInsdPlan 
         Caption         =   "Plan Type"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   2280
         TabIndex        =   25
         Top             =   360
         Width           =   495
      End
      Begin Label lblAddInsdCOLA 
         Caption         =   "COLA percent"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   5760
         TabIndex        =   28
         Top             =   360
         Width           =   735
      End
      Begin Label lblAddInsuredSubStd 
         Alignment       =   2  'Center
         Caption         =   "Table Rating (Substandard)"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   4440
         TabIndex        =   27
         Top             =   360
         Width           =   1095
      End
      Begin Label lblAddInsdFace 
         Alignment       =   2  'Center
         Caption         =   "Death Benefit Amount"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   3240
         TabIndex        =   26
         Top             =   360
         Width           =   1095
      End
      Begin Label lblAddInsdSex 
         Caption         =   "Gender"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Left            =   1320
         TabIndex        =   24
         Top             =   360
         Width           =   615
      End
      Begin Label lblAddInsdAge 
         Alignment       =   2  'Center
         Caption         =   "Issue Age"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Left            =   720
         TabIndex        =   23
         Top             =   360
         Width           =   615
      End
   End
End

Sub cboAddInsdCOLA_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtAddInsdYearStart(Index).SetFocus

End Sub

Sub cboAddInsdEndYearsAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       If Index < 8 Then
          chkAddInsd(Index + 1).SetFocus
       Else
          cmdExit.SetFocus
       End If
    End If

End Sub

Sub cboAddInsdEndYearsAge_LostFocus (Index As Integer)
    
    Dim TempAge1 As Integer
    Dim TempAge2 As Integer
    Dim TempAge3 As Integer

    TempAge1 = Val(txtAddInsdAge(Index).Text)
    If txtAddInsdYearEnd(Index).Text = "" Then
       If cboAddInsdEndYearsAge(Index).ListIndex = 1 Then
          txtAddInsdYearEnd(Index).Text = Str$(65)
       Else
          txtAddInsdYearEnd(Index).Text = Str$(95 - TempAge1)
       End If
    End If
    TempAge2 = Val(txtAddInsdYearEnd(Index).Text)
    Select Case cboAddInsdEndYearsAge(Index).ListIndex
       Case 1
          If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
             TempAge3 = Val(txtAddInsdYearStart(Index).Text)
          Else
             TempAge3 = Val(txtAddInsdYearStart(Index).Text) + TempAge1
          End If
          If TempAge2 < TempAge3 Or TempAge2 > 95 Then
             msgline$ = "End Age for Additional Insured must be greater than or equal to Start Age and less than 95"
             MsgBox msgline$
             txtAddInsdYearEnd(Index).SetFocus
             Exit Sub
          End If
       Case Else
          If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
             TempAge3 = Val(txtAddInsdYearStart(Index).Text) - TempAge1
          Else
             TempAge3 = Val(txtAddInsdYearStart(Index).Text)
          End If
          If TempAge2 > 95 - TempAge1 Or TempAge2 < TempAge3 Then
             msgline$ = "Years for Additional Insured cannot be greater than Insured Age 95 or less than Start Year"
             MsgBox msgline$
             txtAddInsdYearEnd(Index).SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub cboAddInsdPlan_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtAddInsdFace(Index).SetFocus

End Sub

Sub cboAddInsdStartYearsAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       txtAddInsdYearEnd(Index).SetFocus
    End If
    
End Sub

Sub cboAddInsdStartYearsAge_LostFocus (Index As Integer)
    
    Dim TempAge1 As Integer
    Dim TempAge2 As Integer

    TempAge1 = Val(txtAddInsdAge(Index).Text)
    If txtAddInsdYearStart(Index).Text = "" Then
       If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
          txtAddInsdYearStart(Index).Text = Str$(TempAge1)
       Else
          txtAddInsdYearStart(Index).Text = Str$(1)
       End If
    End If
    TempAge2 = Val(txtAddInsdYearStart(Index).Text)
    Select Case cboAddInsdStartYearsAge(Index).ListIndex
       Case 1
          If TempAge2 > 70 Or TempAge2 < TempAge1 Then
             msgline$ = "Start Age for Additional Insured cannot be greater than 70 nor less than Issue Age"
             MsgBox msgline$
             txtAddInsdYearStart(Index).SetFocus
             Exit Sub
          End If
       Case Else
          If TempAge2 < 1 Or TempAge2 + TempAge1 - 1 > 70 Then
             msgline$ = "Start Year for Additional Insured must be less than " & Format$(71 - TempAge1, "##")
             MsgBox msgline$
             txtAddInsdYearStart(Index).SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub cboAddInsdSubStd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtAddInsdYearStart(Index).SetFocus

End Sub

Sub cboChildFace_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then chkSpouse.SetFocus

End Sub

Sub cboSpouseCOLA_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then chkAddInsd(0).SetFocus

End Sub

Sub cboSpouseFace_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtSpouseYears.SetFocus

End Sub

Sub cboSpousePlan_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseFace.SetFocus

End Sub

Sub cboSpouseSubStd_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then chkAddInsd(0).SetFocus

End Sub

Sub cboSpouseYearsAge_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then cboSpouseSubStd.SetFocus

End Sub

Sub cboSpouseYearsAge_LostFocus ()

    Dim TempAge As Integer

    TempAge = Val(txtSpouseAge.Text)
    If txtSpouseYears.Text = "" Then
       If cboSpouseYearsAge.ListIndex = 1 Then
          txtSpouseYears.Text = Str$(65)
       Else
          txtSpouseYears.Text = Str$(95 - TempAge)
       End If
    End If
    Select Case cboSpouseYearsAge.ListIndex
       Case 1
          If Val(txtSpouseYears.Text) <= TempAge Or TempAge > 95 Then
             msgline$ = "Termination Age for Spouse Rider must be greater than Spouse Issue Age and less than 95"
             MsgBox msgline$
             txtSpouseYears.SetFocus
             Exit Sub
          End If
       Case Else
          If Val(txtSpouseYears.Text) > 95 - TempAge Then
             msgline$ = "Years for Spouse Rider cannot be greater than Spouse Age 95"
             MsgBox msgline$
             txtSpouseYears.SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub chkAddInsd_Click (Index As Integer)
    
    If chkAddInsd(Index).Value <> 0 Then
       cboAddInsdPlan(Index).Visible = True
       txtAddInsdFace(Index).Visible = True
       cboAddInsdSubStd(Index).Visible = True
       txtAddInsdAge(Index).Visible = True
       txtAddInsdSex(Index).Visible = True
       txtAddInsdYearStart(Index).Visible = True
       txtAddInsdYearEnd(Index).Visible = True
       cboAddInsdStartYearsAge(Index).Visible = True
       cboAddInsdEndYearsAge(Index).Visible = True
       cboAddInsdCOLA(Index).Visible = True
    Else
       cboAddInsdPlan(Index).Visible = False
       txtAddInsdFace(Index).Visible = False
       cboAddInsdSubStd(Index).Visible = False
       txtAddInsdAge(Index).Visible = False
       txtAddInsdSex(Index).Visible = False
       txtAddInsdYearStart(Index).Visible = False
       txtAddInsdYearEnd(Index).Visible = False
       cboAddInsdStartYearsAge(Index).Visible = False
       cboAddInsdEndYearsAge(Index).Visible = False
       cboAddInsdCOLA(Index).Visible = False
    End If

End Sub

Sub chkAddInsd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       If txtAddInsdAge(Index).Visible = True Then
          txtAddInsdAge(Index).SetFocus
       Else
          If Index < 8 Then
             chkAddInsd(Index + 1).SetFocus
          Else
             cmdExit.SetFocus
          End If
       End If
    End If

End Sub

Sub chkChild_Click ()

    If chkChild.Value <> 0 Then
       cboChildFace.Visible = True
       txtChildAge.Visible = True
    Else
       cboChildFace.Visible = False
       txtChildAge.Visible = False
    End If

End Sub

Sub chkChild_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       If txtChildAge.Visible = True Then
          txtChildAge.SetFocus
       Else
          chkSpouse.SetFocus
       End If
    End If
    
End Sub

Sub chkSpouse_Click ()
    
    If chkSpouse.Value <> 0 Then
       cboSpousePlan.Visible = True
       cboSpouseFace.Visible = True
       cboSpouseSubStd.Visible = True
       txtSpouseAge.Visible = True
       cboSpouseCOLA.Visible = True
       txtSpouseYears.Visible = True
       cboSpouseYearsAge.Visible = True
    Else
       cboSpousePlan.Visible = False
       cboSpouseFace.Visible = False
       cboSpouseSubStd.Visible = False
       txtSpouseAge.Visible = False
       cboSpouseCOLA.Visible = False
       txtSpouseYears.Visible = False
       cboSpouseYearsAge.Visible = False
    End If

End Sub

Sub chkSpouse_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then
       If txtSpouseAge.Visible = True Then
          txtSpouseAge.SetFocus
       Else
          chkAddInsd(0).SetFocus
       End If
    End If

End Sub

Sub cmdExit_Click ()

    Call Set_Insd_Illus_Parameters
    Unload frmLSINSD
    SpouseClient = 0
    frmLIFESV.Show 0

End Sub

Sub cmdFuture_Click ()
    
    Call Set_Insd_Illus_Parameters
    Unload frmLSINSD
    frmLSCHANGE.Show 0

End Sub

Sub cmdPremCalc_Click ()
     
    Call Set_Insd_Illus_Parameters
    Unload frmLSINSD
    frmLSPREM.Show 0

End Sub

Sub cmdRefreshInsureds_Click ()

    Dim YoungestChild As Integer
    Dim InsuredCount As Integer
    Dim Counter As Integer
    Dim RetCode As Integer
    Dim Temp_Age As Integer
    Dim Today As Long
    Dim SerialBDt As Long
    Dim TempDate As String
    Dim CalcYear As Integer
    Dim YMDBdate As Long
    
    chkChild.Value = False
    chkSpouse.Value = False
    For Counter = 0 To 8
      chkAddInsd(Counter).Value = False
    Next Counter

    YoungestChild = 99
    InsuredCount = 0

    If ClientInfo.ClientNumber > 0 Then
       For Counter = 1 To 12
         RetCode = insured_file(1, ClientInfo.ClientNumber, Counter)
         If RetCode Then
            If InsuredInfo.InsRelation = "C" Then
               GoSub Figure_Age
               YoungestChild = Temp_Age
               chkChild.Value = 1
            End If
            If InsuredInfo.InsRelation = "S" Then
               GoSub Figure_Age
               chkSpouse.Value = 1
               txtSpouseAge.Text = Right$(Str$(Temp_Age), 2)
            End If
            If InsuredInfo.InsRelation = "O" Then
               GoSub Figure_Age
               chkAddInsd(InsuredCount).Value = 1
               txtAddInsdAge(InsuredCount).Text = Right$(Str$(Temp_Age), 2)
               txtAddInsdSex(InsuredCount).Text = InsuredInfo.InsGender
               InsuredCount = InsuredCount + 1
            End If
         End If
       Next Counter
       If YoungestChild < 99 Then
          txtChildAge.Text = Right$(Str$(YoungestChild), 2)
       End If
    End If
    Exit Sub

Figure_Age:

    Today = Fix(Now)
    SerialBDt = InsuredInfo.InsBirthDate
    Call Year_Calc(SerialBDt, Today, CalcYear, YMDBdate)
    Temp_Age = CalcYear
    Return

End Sub

Sub Form_Load ()

    cboChildFace.AddItem "10,000"
    cboChildFace.AddItem "7,500"
    cboChildFace.AddItem "5,000"
    cboChildFace.AddItem "2,500"
    cboChildFace.ListIndex = 0
    cboSpousePlan.AddItem "NonSmoker"
    cboSpousePlan.AddItem "Smoker"
    cboSpousePlan.ListIndex = 0
    cboSpouseFace.AddItem "50,000"
    cboSpouseFace.AddItem "25,000"
    cboSpouseFace.ListIndex = 0
    cboSpouseYearsAge.AddItem "Years"
    cboSpouseYearsAge.AddItem "Age"
    cboSpouseYearsAge.ListIndex = 0
    cboSpouseSubStd.AddItem "None"
    cboSpouseSubStd.AddItem "1.25"
    cboSpouseSubStd.AddItem "1.50"
    cboSpouseSubStd.AddItem "1.75"
    cboSpouseSubStd.AddItem "2.00"
    cboSpouseSubStd.AddItem "2.25"
    cboSpouseSubStd.AddItem "2.50"
    cboSpouseSubStd.AddItem "2.75"
    cboSpouseSubStd.AddItem "3.00"
    cboSpouseSubStd.AddItem "3.25"
    cboSpouseSubStd.AddItem "3.50"
    cboSpouseSubStd.AddItem "3.75"
    cboSpouseSubStd.AddItem "4.00"
    cboSpouseSubStd.AddItem "4.25"
    cboSpouseSubStd.AddItem "4.50"
    cboSpouseSubStd.AddItem "4.75"
    cboSpouseSubStd.AddItem "5.00"
    cboSpouseSubStd.ListIndex = 0
    cboSpouseCOLA.AddItem "None"
    cboSpouseCOLA.AddItem "1%"
    cboSpouseCOLA.AddItem "2%"
    cboSpouseCOLA.AddItem "3%"
    cboSpouseCOLA.AddItem "4%"
    cboSpouseCOLA.AddItem "5%"
    cboSpouseCOLA.AddItem "6%"
    cboSpouseCOLA.AddItem "7%"
    cboSpouseCOLA.AddItem "8%"
    cboSpouseCOLA.AddItem "9%"
    cboSpouseCOLA.AddItem "10%"
    cboSpouseCOLA.ListIndex = LSClient(SpouseClient).COLA
    For i% = 0 To 8
      cboAddInsdPlan(i%).AddItem "NonSmoker"
      cboAddInsdPlan(i%).AddItem "Smoker"
      cboAddInsdPlan(i%).ListIndex = 0
      cboAddInsdSubStd(i%).AddItem "None"
      cboAddInsdSubStd(i%).AddItem "1.25"
      cboAddInsdSubStd(i%).AddItem "1.50"
      cboAddInsdSubStd(i%).AddItem "1.75"
      cboAddInsdSubStd(i%).AddItem "2.00"
      cboAddInsdSubStd(i%).AddItem "2.25"
      cboAddInsdSubStd(i%).AddItem "2.50"
      cboAddInsdSubStd(i%).AddItem "2.75"
      cboAddInsdSubStd(i%).AddItem "3.00"
      cboAddInsdSubStd(i%).AddItem "3.25"
      cboAddInsdSubStd(i%).AddItem "3.50"
      cboAddInsdSubStd(i%).AddItem "3.75"
      cboAddInsdSubStd(i%).AddItem "4.00"
      cboAddInsdSubStd(i%).AddItem "4.25"
      cboAddInsdSubStd(i%).AddItem "4.50"
      cboAddInsdSubStd(i%).AddItem "4.75"
      cboAddInsdSubStd(i%).AddItem "5.00"
      cboAddInsdSubStd(i%).ListIndex = 0
      cboAddInsdCOLA(i%).AddItem "None"
      cboAddInsdCOLA(i%).AddItem "1%"
      cboAddInsdCOLA(i%).AddItem "2%"
      cboAddInsdCOLA(i%).AddItem "3%"
      cboAddInsdCOLA(i%).AddItem "4%"
      cboAddInsdCOLA(i%).AddItem "5%"
      cboAddInsdCOLA(i%).AddItem "6%"
      cboAddInsdCOLA(i%).AddItem "7%"
      cboAddInsdCOLA(i%).AddItem "8%"
      cboAddInsdCOLA(i%).AddItem "9%"
      cboAddInsdCOLA(i%).AddItem "10%"
      cboAddInsdCOLA(i%).ListIndex = LSClient(SpouseClient).COLA
      cboAddInsdStartYearsAge(i%).AddItem "Year"
      cboAddInsdStartYearsAge(i%).AddItem "Age"
      cboAddInsdEndYearsAge(i%).AddItem "Year"
      cboAddInsdEndYearsAge(i%).AddItem "Age"
      cboAddInsdStartYearsAge(i%).ListIndex = 0
      cboAddInsdEndYearsAge(i%).ListIndex = 0
    Next i%

    Call Load_Insureds

End Sub

Sub Form_Unload (Cancel As Integer)

    Set frmLSINSD = Nothing

End Sub

Sub txtAddInsdAge_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtAddInsdAge(Index).Text)
    txtAddInsdAge(Index).Text = ""
    txtAddInsdAge(Index).Text = clipboard.GetText()
    txtAddInsdAge(Index).ForeColor = &H80000005
    txtAddInsdAge(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             If KeyCode = KEY_HOME Then chkChild.SetFocus
             If KeyCode = KEY_END Then cmdRefreshInsureds.SetFocus
          End If
       Case KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then cboAddInsdPlan(Index).SetFocus
          If KeyCode = KEY_ESCAPE Then
             txtAddInsdAge(Index).Text = clipboard.GetText()
             cboAddInsdPlan(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtAddInsdAge(Index).Text = clipboard.GetText() Then
          txtAddInsdAge(Index).Text = ""
       End If
    End If

End Sub

Sub txtAddInsdAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Len(txtAddInsdAge(Index).Text) >= 2 Then
          Call txtAddInsdAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtAddInsdAge_LostFocus (Index As Integer)

    Dim TempAge As Integer

    TempAge = Val(txtAddInsdAge(Index).Text)
    If TempAge = 0 Then txtAddInsdAge(Index).Text = "0"
    msgtext$ = ""
    If TempAge > 70 Then
       msgtext$ = "Additional Insured Age must be between 0 and 70, inclusive, for Life Savings II"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtAddInsdAge(Index).Text = ""
       txtAddInsdAge(Index).SetFocus
       Exit Sub
    End If
    
    If txtAddInsdYearStart(Index).Text = "" Then
       If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
          txtAddInsdYearStart(Index).Text = Str$(TempAge)
       Else
          txtAddInsdYearStart(Index).Text = Str$(1)
       End If
    End If
    If txtAddInsdYearEnd(Index).Text = "" Then
       If cboAddInsdEndYearsAge(Index).ListIndex = 1 Then
          txtAddInsdYearEnd(Index).Text = Str$(65)
       Else
          txtAddInsdYearEnd(Index).Text = Str$(95 - TempAge)
       End If
    End If
    
    txtAddInsdSex(Index).SetFocus
    txtAddInsdAge(Index).ForeColor = &H80000008
    txtAddInsdAge(Index).BackColor = &H80000005

End Sub

Sub txtAddInsdFace_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtAddInsdFace(Index).Text)
    txtAddInsdFace(Index).Text = "0000000"
    txtAddInsdFace(Index).Text = clipboard.GetText()
    txtAddInsdFace(Index).ForeColor = &H80000005
    txtAddInsdFace(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdFace_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChild.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRefreshInsureds.SetFocus
    If KeyCode = KEY_RETURN Then cboAddInsdSubStd(Index).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtAddInsdFace(Index).Text = clipboard.GetText()
       cboAddInsdSubStd(Index).SetFocus
    End If

End Sub

Sub txtAddInsdFace_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then KeyAscii = 0

End Sub

Sub txtAddInsdFace_LostFocus (Index As Integer)

    Dim TempAge As Integer
    Dim TempAmt As Single
    Dim MinFace As String

    TempAge = Val(txtAddInsdAge(Index).Text)
    txtAddInsdFace(Index).ForeColor = &H80000008
    txtAddInsdFace(Index).BackColor = &H80000005
    TempAmt = Val(txtAddInsdFace(Index).Text)
    If TempAmt = 0 Then txtAddInsdFace(Index).Text = "0"
    If TempAmt < 25000 Or TempAmt > 5000000 Then
       Select Case TempAge
          Case Is < 51
             MinFace = "$25,000"
          Case Is < 61
             MinFace = "$20,000"
          Case Else
             MinFace = "$15,000"
       End Select
       If TempAmt > 5000000 Or (TempAge < 51 And TempAmt < 25000) Or (TempAge < 61 And TempAmt < 20000) Or (TempAge > 60 And TempAmt < 15000) Then
          msgline$ = "Additional Insured Face Amount must be between " & MinFace & " and $5,000,000, inclusive"
          MsgBox msgline$
          txtAddInsdFace(Index).Text = ""
          txtAddInsdFace(Index).SetFocus
          Exit Sub
       End If
    End If

End Sub

Sub txtAddInsdSex_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtAddInsdSex(Index).Text)
    txtAddInsdSex(Index).Text = ""
    txtAddInsdSex(Index).Text = clipboard.GetText()
    txtAddInsdSex(Index).ForeColor = &H80000005
    txtAddInsdSex(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdSex_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChild.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRefreshInsureds.SetFocus
    If KeyCode = KEY_RETURN Then cboAddInsdPlan(Index).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtAddInsdSex(Index).Text = clipboard.GetText()
       cboAddInsdPlan(Index).SetFocus
    End If

End Sub

Sub txtAddInsdSex_KeyPress (Index As Integer, KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtAddInsdSex(Index).Text = clipboard.GetText() Then
             txtAddInsdSex(Index).Text = ""
             cboAddInsdPlan(Index).SetFocus
          End If
       Case Else
          KeyAscii = 0
    End Select

End Sub

Sub txtAddInsdSex_LostFocus (Index As Integer)
    
    txtAddInsdSex(Index).ForeColor = &H80000008
    txtAddInsdSex(Index).BackColor = &H80000005
    txtAddInsdSex(Index).Text = UCase$(txtAddInsdSex(Index).Text)

End Sub

Sub txtAddInsdYearEnd_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtAddInsdYearEnd(Index).Text)
    txtAddInsdYearEnd(Index).Text = ""
    txtAddInsdYearEnd(Index).Text = clipboard.GetText()
    txtAddInsdYearEnd(Index).ForeColor = &H80000005
    txtAddInsdYearEnd(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdYearEnd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             If KeyCode = KEY_HOME Then chkChild.SetFocus
             If KeyCode = KEY_END Then cmdRefreshInsureds.SetFocus
          End If
       Case KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then cboAddInsdEndYearsAge(Index).SetFocus
          If KeyCode = KEY_ESCAPE Then
             txtAddInsdYearEnd(Index).Text = clipboard.GetText()
             cboAddInsdEndYearsAge(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdYearEnd_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii > 47 And KeyAscii < 57 Then
       If txtAddInsdYearEnd(Index).Text = clipboard.GetText() Then
          txtAddInsdYearEnd(Index).Text = ""
       End If
       If Len(txtAddInsdYearEnd(Index).Text) > 1 Then cboAddInsdEndYearsAge(Index).SetFocus
    End If

End Sub

Sub txtAddInsdYearEnd_LostFocus (Index As Integer)

    txtAddInsdYearEnd(Index).ForeColor = &H80000008
    txtAddInsdYearEnd(Index).BackColor = &H80000005

End Sub

Sub txtAddInsdYearStart_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtAddInsdYearStart(Index).Text)
    txtAddInsdYearStart(Index).Text = ""
    txtAddInsdYearStart(Index).Text = clipboard.GetText()
    txtAddInsdYearStart(Index).ForeColor = &H80000005
    txtAddInsdYearStart(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdYearStart_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             If KeyCode = KEY_HOME Then chkChild.SetFocus
             If KeyCode = KEY_END Then cmdRefreshInsureds.SetFocus
          End If
       Case KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then cboAddInsdStartYearsAge(Index).SetFocus
          If KeyCode = KEY_ESCAPE Then
             txtAddInsdYearStart(Index).Text = clipboard.GetText()
             cboAddInsdStartYearsAge(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdYearStart_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii > 47 And KeyAscii < 57 Then
       If txtAddInsdYearStart(Index).Text = clipboard.GetText() Then
          txtAddInsdYearStart(Index).Text = ""
       End If
       If Len(txtAddInsdYearStart(Index).Text) > 1 Then cboAddInsdStartYearsAge(Index).SetFocus
    End If

End Sub

Sub txtAddInsdYearStart_LostFocus (Index As Integer)

    txtAddInsdYearStart(Index).ForeColor = &H80000008
    txtAddInsdYearStart(Index).BackColor = &H80000005

End Sub

Sub txtChildAge_GotFocus ()
 
    clipboard.Clear
    clipboard.SetText Trim$(txtChildAge.Text)
    txtChildAge.Text = ""
    txtChildAge.Text = clipboard.GetText()
    txtChildAge.ForeColor = &H80000005
    txtChildAge.BackColor = &H80000008

End Sub

Sub txtChildAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             If KeyCode = KEY_HOME Then chkChild.SetFocus
             If KeyCode = KEY_END Then cmdRefreshInsureds.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then cboChildFace.SetFocus
          If KeyCode = KEY_ESCAPE Then
             txtChildAge.Text = clipboard.GetText()
             cboChildFace.SetFocus
          End If
          If KeyCode = KEY_DOWN Then chkAddInsd(0).SetFocus
          If KeyCode = KEY_UP Then chkChild.SetFocus
    End Select

End Sub

Sub txtChildAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChildAge.Text = clipboard.GetText() Then
          txtChildAge.Text = ""
       End If
    End If

End Sub

Sub txtChildAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Len(txtChildAge.Text) >= 2 Then
          Call txtChildAge_LostFocus
       End If
    End If

End Sub

Sub txtChildAge_LostFocus ()

    Dim TempAge As Integer

    TempAge = Val(txtChildAge.Text)
    msgtext$ = ""
    If TempAge > 20 Then
       msgtext$ = "Age Youngest Child must be no more than 20 for Life Savings II"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtChildAge.Text = ""
       txtChildAge.SetFocus
       Exit Sub
    End If
    cboChildFace.SetFocus
    txtChildAge.ForeColor = &H80000008
    txtChildAge.BackColor = &H80000005

End Sub

Sub txtSpouseAge_GotFocus ()
 
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseAge.Text)
    txtSpouseAge.Text = ""
    txtSpouseAge.Text = clipboard.GetText()
    txtSpouseAge.ForeColor = &H80000005
    txtSpouseAge.BackColor = &H80000008

End Sub

Sub txtSpouseAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    Select Case (KeyCode)
       Case KEY_HOME, KEY_END
          If Shift = 4 Then
             If KeyCode = KEY_HOME Then chkChild.SetFocus
             If KeyCode = KEY_END Then cmdRefreshInsureds.SetFocus
          End If
       Case KEY_DOWN, KEY_UP, KEY_RETURN, KEY_ESCAPE
          If KeyCode = KEY_RETURN Then cboSpousePlan.SetFocus
          If KeyCode = KEY_ESCAPE Then
             txtSpouseAge.Text = clipboard.GetText()
             cboSpousePlan.SetFocus
          End If
          If KeyCode = KEY_DOWN Then chkAddInsd(0).SetFocus
          If KeyCode = KEY_UP Then chkSpouse.SetFocus
    End Select

End Sub

Sub txtSpouseAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtSpouseAge.Text = clipboard.GetText() Then
          txtSpouseAge.Text = ""
       End If
    End If

End Sub

Sub txtSpouseAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Len(txtSpouseAge.Text) >= 2 Then
          Call txtSpouseAge_LostFocus
       End If
    End If

End Sub

Sub txtSpouseAge_LostFocus ()

    Dim TempAge As Integer

    TempAge = Val(txtSpouseAge.Text)
    msgtext$ = ""
    If TempAge < 15 Or TempAge > 65 Then
       msgtext$ = "Spouse Age must be between 15 and 65, inclusive, for Life Savings II"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtSpouseAge.Text = ""
       txtSpouseAge.SetFocus
       Exit Sub
    End If
    
    If txtSpouseYears.Text = "" Then
       If cboSpouseYearsAge.ListIndex = 1 Then
          txtSpouseYears.Text = Str$(65)
       Else
          txtSpouseYears.Text = Str$(95 - TempAge)
       End If
    End If
    
    cboSpousePlan.SetFocus
    txtSpouseAge.ForeColor = &H80000008
    txtSpouseAge.BackColor = &H80000005

End Sub

Sub txtSpouseYears_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSpouseYears.Text)
    txtSpouseYears.Text = "0000000"
    txtSpouseYears.Text = clipboard.GetText()
    txtSpouseYears.ForeColor = &H80000005
    txtSpouseYears.BackColor = &H80000008

End Sub

Sub txtSpouseYears_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChild.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRefreshInsureds.SetFocus
    If KeyCode = KEY_RETURN Then cboSpouseYearsAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSpouseYears.Text = clipboard.GetText()
       cboSpouseYearsAge.SetFocus
    End If

End Sub

Sub txtSpouseYears_KeyPress (KeyAscii As Integer)

    If KeyAscii > 47 And KeyAscii < 57 Then
       If txtSpouseYears.Text = clipboard.GetText() Then
          txtSpouseYears.Text = ""
       End If
       If Len(txtSpouseYears.Text) > 1 Then cboSpouseYearsAge.SetFocus
    End If

End Sub

Sub txtSpouseYears_LostFocus ()
    
    txtSpouseYears.ForeColor = &H80000008
    txtSpouseYears.BackColor = &H80000005

End Sub

