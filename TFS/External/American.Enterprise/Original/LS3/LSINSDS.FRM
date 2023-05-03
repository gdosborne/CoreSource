VERSION 2.00
Begin Form frmLSINSD 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "LifeSavings - Child Rider and Additional Insureds"
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Height          =   7320
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6915
   ScaleWidth      =   9585
   Top             =   75
   Width           =   9705
   Begin Frame fraAddInsdChild 
      Caption         =   "Insured Child(ren) Rider"
      ClipControls    =   0   'False
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1332
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   9252
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
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Left            =   3360
         Style           =   2  'Dropdown List
         TabIndex        =   3
         Top             =   840
         Visible         =   0   'False
         Width           =   1215
      End
      Begin MaskEdBox txtChildAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   288
         Left            =   1800
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   2
         Top             =   840
         Visible         =   0   'False
         Width           =   372
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
         Height          =   372
         Left            =   3360
         TabIndex        =   100
         Top             =   360
         Width           =   1212
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
         Height          =   372
         Left            =   1440
         TabIndex        =   99
         Top             =   360
         Width           =   1092
      End
   End
   Begin Frame fraAddInsureds 
      Caption         =   "Additional Insureds (Must be immediate family members)"
      ClipControls    =   0   'False
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   4215
      Left            =   120
      TabIndex        =   4
      Top             =   1680
      Width           =   9255
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
         TabIndex        =   92
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
         TabIndex        =   81
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
         TabIndex        =   72
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
         TabIndex        =   62
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
         TabIndex        =   52
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
         TabIndex        =   42
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
         TabIndex        =   32
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
         TabIndex        =   22
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
         TabIndex        =   12
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
         TabIndex        =   94
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
         TabIndex        =   83
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
         TabIndex        =   74
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
         TabIndex        =   64
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
         TabIndex        =   54
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
         TabIndex        =   44
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
         TabIndex        =   34
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
         TabIndex        =   24
         Top             =   1080
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
         Index           =   0
         Left            =   8400
         Style           =   2  'Dropdown List
         TabIndex        =   14
         Top             =   720
         Visible         =   0   'False
         Width           =   735
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   8
         Top             =   720
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   88
         Top             =   3600
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   78
         Top             =   3240
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   68
         Top             =   2880
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   58
         Top             =   2520
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   48
         Top             =   2160
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   38
         Top             =   1800
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   28
         Top             =   1440
         Visible         =   0   'False
         Width           =   1455
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
         Left            =   2160
         Style           =   2  'Dropdown List
         TabIndex        =   18
         Top             =   1080
         Visible         =   0   'False
         Width           =   1455
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   8
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   90
         Top             =   3600
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   7
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   84
         Top             =   3240
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   6
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   70
         Top             =   2880
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   5
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   60
         Top             =   2520
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   4
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   50
         Top             =   2160
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   3
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   40
         Top             =   1800
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   2
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   30
         Top             =   1440
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   1
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   20
         Top             =   1080
         Visible         =   0   'False
         Width           =   855
      End
      Begin ComboBox cboAddInsdSubStd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   300
         Index           =   0
         Left            =   5400
         Style           =   2  'Dropdown List
         TabIndex        =   10
         Top             =   720
         Visible         =   0   'False
         Width           =   855
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   8
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   87
         Text            =   " "
         Top             =   3600
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   7
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   77
         Text            =   " "
         Top             =   3240
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   6
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   67
         Text            =   " "
         Top             =   2880
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   5
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   57
         Text            =   " "
         Top             =   2520
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   4
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   47
         Text            =   " "
         Top             =   2160
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   3
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   37
         Text            =   " "
         Top             =   1800
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   2
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   27
         Text            =   " "
         Top             =   1440
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   1
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   17
         Text            =   " "
         Top             =   1080
         Visible         =   0   'False
         Width           =   255
      End
      Begin TextBox txtAddInsdSex 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   0
         Left            =   1560
         MaxLength       =   1
         TabIndex        =   7
         Text            =   " "
         Top             =   720
         Visible         =   0   'False
         Width           =   255
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(1)"
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   5
         Top             =   720
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(5)"
         Height          =   255
         Index           =   4
         Left            =   120
         TabIndex        =   45
         Top             =   2160
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(4)"
         Height          =   255
         Index           =   3
         Left            =   120
         TabIndex        =   35
         Top             =   1800
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(3)"
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   25
         Top             =   1440
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(2)"
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   15
         Top             =   1080
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(6)"
         Height          =   255
         Index           =   5
         Left            =   120
         TabIndex        =   55
         Top             =   2520
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(7)"
         Height          =   255
         Index           =   6
         Left            =   120
         TabIndex        =   65
         Top             =   2880
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(8)"
         Height          =   255
         Index           =   7
         Left            =   120
         TabIndex        =   75
         Top             =   3240
         Width           =   615
      End
      Begin CheckBox chkAddInsd 
         Caption         =   "(9)"
         Height          =   255
         Index           =   8
         Left            =   120
         TabIndex        =   85
         Top             =   3600
         Width           =   615
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   8
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   91
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   7
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   80
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   6
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   71
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   5
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   61
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   4
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   51
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   3
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   41
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   2
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   31
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   1
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   21
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearStart 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   0
         Left            =   6720
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   11
         Top             =   720
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   8
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   93
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   7
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   82
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   6
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   73
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   5
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   63
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   4
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   53
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   3
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   43
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   2
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   33
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   1
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   23
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   2
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   29
         Top             =   1440
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdYearEnd 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   0
         Left            =   8040
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   13
         Top             =   720
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   8
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   89
         Top             =   3600
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   7
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   79
         Top             =   3240
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   6
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   69
         Top             =   2880
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   5
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   59
         Top             =   2520
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   4
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   49
         Top             =   2160
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   3
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   39
         Top             =   1800
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   1
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   19
         Top             =   1080
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdFace 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   0
         Left            =   3960
         Mask            =   "#######"
         MaxLength       =   7
         PromptChar      =   "_"
         PromptInclude   =   0   'False
         TabIndex        =   9
         Top             =   720
         Visible         =   0   'False
         Width           =   975
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Index           =   8
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   86
         Top             =   3600
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Index           =   7
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   76
         Top             =   3240
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   6
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   66
         Top             =   2880
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   5
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   56
         Top             =   2520
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   4
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   46
         Top             =   2160
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   3
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   36
         Top             =   1800
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   2
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   26
         Top             =   1440
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   1
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   16
         Top             =   1080
         Visible         =   0   'False
         Width           =   375
      End
      Begin MaskEdBox txtAddInsdAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Index           =   0
         Left            =   840
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   " "
         PromptInclude   =   0   'False
         TabIndex        =   6
         Top             =   720
         Visible         =   0   'False
         Width           =   375
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
         TabIndex        =   108
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
         TabIndex        =   109
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
         TabIndex        =   101
         Top             =   360
         Width           =   615
      End
      Begin Label lblInsuredTermRider 
         Alignment       =   2  'Center
         Caption         =   "--- Add-on and Removal Years --"
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   6600
         TabIndex        =   107
         Top             =   180
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
         Left            =   2640
         TabIndex        =   104
         Top             =   360
         Width           =   495
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
         Height          =   372
         Left            =   5280
         TabIndex        =   106
         Top             =   360
         Width           =   1092
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
         Left            =   3840
         TabIndex        =   105
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
         Height          =   252
         Left            =   1440
         TabIndex        =   103
         Top             =   360
         Width           =   612
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
         TabIndex        =   102
         Top             =   360
         Width           =   615
      End
   End
   Begin SSCommand cmdFuture 
      BevelWidth      =   4
      Caption         =   "&FUTURE Changes"
      Height          =   735
      Left            =   4920
      TabIndex        =   97
      Top             =   6000
      Width           =   1815
   End
   Begin SSCommand cmdRefreshInsureds 
      BevelWidth      =   4
      Caption         =   "&REFRESH "
      Height          =   735
      Left            =   7080
      TabIndex        =   98
      Top             =   6000
      Width           =   1695
   End
   Begin SSCommand cmdPremCalc 
      BevelWidth      =   4
      Caption         =   "&PREMIUM CALC"
      Height          =   735
      Left            =   2760
      TabIndex        =   96
      Top             =   6000
      Width           =   1815
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      Height          =   735
      Left            =   480
      TabIndex        =   95
      Top             =   6000
      Width           =   1935
   End
End

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
    Dim ClientIssueAge As Integer
    Dim Base_95 As Integer
    
    ClientIssueAge = Val(frmLIFESV.txtClientAge)
    TempAge1 = Val(txtAddInsdAge(Index).Text)
    If ClientIssueAge > TempAge1 Then
       Base_95 = 95 - (ClientIssueAge - TempAge1)
    Else
       Base_95 = 95
    End If

    If txtAddInsdYearEnd(Index).Text = "" Then
       If cboAddInsdEndYearsAge(Index).ListIndex = 1 Then
          txtAddInsdYearEnd(Index).Text = Trim$(Str$(Base_95))
       Else
          txtAddInsdYearEnd(Index).Text = Trim$(Str$(Base_95 - TempAge1))
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
          If TempAge2 < TempAge3 Or TempAge2 > Base_95 Then
             MsgText = "End Age for Additional Insured must be greater than Start Age and less than Client or Insured Age 95."
             MsgBox MsgText, , "LifeSavings"
             txtAddInsdYearEnd(Index).SetFocus
             Exit Sub
          End If
       Case Else
          If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
             TempAge3 = Val(txtAddInsdYearStart(Index).Text) - TempAge1
          Else
             TempAge3 = Val(txtAddInsdYearStart(Index).Text)
          End If
          If TempAge2 > Base_95 - TempAge1 Or TempAge2 < TempAge3 Then
             MsgText = "Years for Additional Insured cannot be greater than Client or Insured Age 95 or less than Start Year."
             MsgBox MsgText, , "LifeSavings"
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
          txtAddInsdYearStart(Index).Text = Trim$(Str$(TempAge1))
       Else
          txtAddInsdYearStart(Index).Text = Str$(1)
       End If
    End If
    TempAge2 = Val(txtAddInsdYearStart(Index).Text)
    Select Case cboAddInsdStartYearsAge(Index).ListIndex
       Case 1
          If TempAge2 > 70 Or TempAge2 < TempAge1 Then
             MsgText = "Start Age for Additional Insured cannot be greater than 70 nor less than Issue Age"
             MsgBox MsgText, , "LifeSavings"
             txtAddInsdYearStart(Index).SetFocus
             Exit Sub
          End If
       Case Else
          If TempAge2 < 1 Or TempAge2 + TempAge1 - 1 > 70 Then
             MsgText = "Start Year for Additional Insured must be less than " & Format$(71 - TempAge1, "##")
             MsgBox MsgText, , "LifeSavings"
             txtAddInsdYearStart(Index).SetFocus
             Exit Sub
          End If
    End Select

End Sub

Sub cboAddInsdSubStd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then txtAddInsdYearStart(Index).SetFocus

End Sub

Sub cboChildFace_KeyDown (KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_RETURN Then chkAddInsd(0).SetFocus

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
       'cboAddInsdCOLA(Index).Visible = True
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
       'cboAddInsdCOLA(Index).Visible = False
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
    Dim Temp_Age As Integer
    Dim Today As Long
    Dim SerialBDt As Long
    Dim TempDate As String
    Dim CalcYear As Integer
    Dim YMDBdate As Long
    
    chkChild.Value = False
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
            If InsuredInfo.InsRelation = "O" Or InsuredInfo.InsRelation = "S" Then
               GoSub Figure_Age
               chkAddInsd(InsuredCount).Value = 1
               txtAddInsdAge(InsuredCount).Text = Right$(Str$(Temp_Age), 2)
               txtAddInsdSex(InsuredCount).Text = InsuredInfo.insGender
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
    SerialBDt = InsuredInfo.InsBirthdate
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
    For i% = 0 To 8
      cboAddInsdPlan(i%).AddItem "No Tobacco Use"
      cboAddInsdPlan(i%).AddItem "Tobacco User"
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
      'cboAddInsdCOLA(i%).AddItem "None"
      'cboAddInsdCOLA(i%).AddItem "1%"
      'cboAddInsdCOLA(i%).AddItem "2%"
      'cboAddInsdCOLA(i%).AddItem "3%"
      'cboAddInsdCOLA(i%).AddItem "4%"
      'cboAddInsdCOLA(i%).AddItem "5%"
      'cboAddInsdCOLA(i%).AddItem "6%"
      'cboAddInsdCOLA(i%).AddItem "7%"
      'cboAddInsdCOLA(i%).AddItem "8%"
      'cboAddInsdCOLA(i%).AddItem "9%"
      'cboAddInsdCOLA(i%).AddItem "10%"
      'cboAddInsdCOLA(i%).ListIndex = LSClient(SpouseClient).COLA
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtAddInsdAge(Index).Text)
    txtAddInsdAge(Index).Text = ""
    txtAddInsdAge(Index).Text = Clipboard.GetText()
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
             txtAddInsdAge(Index).Text = Clipboard.GetText()
             cboAddInsdPlan(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtAddInsdAge(Index).Text = Clipboard.GetText() Then
          txtAddInsdAge(Index).Text = ""
       End If
    End If

End Sub

Sub txtAddInsdAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Len(txtAddInsdAge(Index).Text) >= 2 Then
          Call txtAddInsdAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtAddInsdAge_LostFocus (Index As Integer)

    Dim TempAge As Integer
    Dim ClientIssueAge As Integer
    Dim Base_95 As Integer
    
    ClientIssueAge = Val(frmLIFESV.txtClientAge)
    TempAge = Val(txtAddInsdAge(Index).Text)
    If ClientIssueAge > TempAge Then
       Base_95 = 95 - (ClientIssueAge - TempAge)
    Else
       Base_95 = 95
    End If

    If TempAge = 0 Then txtAddInsdAge(Index).Text = "0"
    MsgText = ""
    If TempAge > 70 Then
       MsgText = "Additional Insured Age must be between 0 and 70, inclusive, for LifeSavings."
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtAddInsdAge(Index).Text = ""
       txtAddInsdAge(Index).SetFocus
       Exit Sub
    End If
    
    If cboAddInsdStartYearsAge(Index).ListIndex = 1 Then
       txtAddInsdYearStart(Index).Text = Trim$(Str$(TempAge))
    Else
       txtAddInsdYearStart(Index).Text = Str$(1)
    End If
    If cboAddInsdEndYearsAge(Index).ListIndex = 1 Then
       txtAddInsdYearEnd(Index).Text = Trim$(Str$(Base_95))
    Else
       txtAddInsdYearEnd(Index).Text = Trim$(Str$(Base_95 - TempAge))
    End If
    
    txtAddInsdSex(Index).SetFocus
    txtAddInsdAge(Index).ForeColor = &H80000008
    txtAddInsdAge(Index).BackColor = &H80000005

End Sub

Sub txtAddInsdFace_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtAddInsdFace(Index).Text)
    txtAddInsdFace(Index).Text = "0000000"
    txtAddInsdFace(Index).Text = Clipboard.GetText()
    txtAddInsdFace(Index).ForeColor = &H80000005
    txtAddInsdFace(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdFace_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChild.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRefreshInsureds.SetFocus
    If KeyCode = KEY_RETURN Then cboAddInsdSubStd(Index).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtAddInsdFace(Index).Text = Clipboard.GetText()
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
    If TempAmt < 25000 Or TempAmt >= 10000000 Then
       Select Case TempAge
          Case Is < 51
             MinFace = "$15,000"   'was $25,000
          Case Is < 61
             MinFace = "$15,000"   'was $20,000
          Case Else
             MinFace = "$15,000"
       End Select
       'If TempAmt > 5000000 Or (TempAge < 51 And TempAmt < 25000) Or (TempAge < 61 And TempAmt < 20000) Or (TempAge > 60 And TempAmt < 15000) Then
       If TempAmt >= 10000000 Or TempAmt < 15000 Then
          MsgText = "Additional Insured Face Amount must be between " & MinFace & " and $9,999,999, inclusive"
          MsgBox MsgText, , "LifeSavings"
          txtAddInsdFace(Index).Text = ""
          txtAddInsdFace(Index).SetFocus
          Exit Sub
       End If
    End If

End Sub

Sub txtAddInsdSex_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtAddInsdSex(Index).Text)
    txtAddInsdSex(Index).Text = ""
    txtAddInsdSex(Index).Text = Clipboard.GetText()
    txtAddInsdSex(Index).ForeColor = &H80000005
    txtAddInsdSex(Index).BackColor = &H80000008

End Sub

Sub txtAddInsdSex_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChild.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRefreshInsureds.SetFocus
    If KeyCode = KEY_RETURN Then cboAddInsdPlan(Index).SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtAddInsdSex(Index).Text = Clipboard.GetText()
       cboAddInsdPlan(Index).SetFocus
    End If

End Sub

Sub txtAddInsdSex_KeyPress (Index As Integer, KeyAscii As Integer)
     
    Select Case (KeyAscii)
       Case 32, 70, 77, 102, 109
          If txtAddInsdSex(Index).Text = Clipboard.GetText() Then
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtAddInsdYearEnd(Index).Text)
    txtAddInsdYearEnd(Index).Text = ""
    txtAddInsdYearEnd(Index).Text = Clipboard.GetText()
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
             txtAddInsdYearEnd(Index).Text = Clipboard.GetText()
             cboAddInsdEndYearsAge(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdYearEnd_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii > 47 And KeyAscii < 57 Then
       If txtAddInsdYearEnd(Index).Text = Clipboard.GetText() Then
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtAddInsdYearStart(Index).Text)
    txtAddInsdYearStart(Index).Text = ""
    txtAddInsdYearStart(Index).Text = Clipboard.GetText()
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
             txtAddInsdYearStart(Index).Text = Clipboard.GetText()
             cboAddInsdStartYearsAge(Index).SetFocus
          End If
    End Select

End Sub

Sub txtAddInsdYearStart_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii > 47 And KeyAscii < 57 Then
       If txtAddInsdYearStart(Index).Text = Clipboard.GetText() Then
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChildAge.Text)
    txtChildAge.Text = ""
    txtChildAge.Text = Clipboard.GetText()
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
             txtChildAge.Text = Clipboard.GetText()
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
       If txtChildAge.Text = Clipboard.GetText() Then
          txtChildAge.Text = ""
       End If
    End If

End Sub

Sub txtChildAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Len(txtChildAge.Text) >= 2 Then
          Call txtChildAge_LostFocus
       End If
    End If

End Sub

Sub txtChildAge_LostFocus ()

    Dim TempAge As Integer

    TempAge = Val(txtChildAge.Text)
    MsgText = ""
    If TempAge > 20 Then
       MsgText = "Age Youngest Child must be no more than 20 for LifeSavings"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtChildAge.Text = ""
       txtChildAge.SetFocus
       Exit Sub
    End If
    cboChildFace.SetFocus
    txtChildAge.ForeColor = &H80000008
    txtChildAge.BackColor = &H80000005

End Sub

