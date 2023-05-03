VERSION 2.00
Begin Form frmLSPREM 
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Life Savings II - Premium Calculation"
   ClientHeight    =   8025
   ClientLeft      =   105
   ClientTop       =   735
   ClientWidth     =   11880
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Height          =   8430
   Left            =   45
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   8025
   ScaleWidth      =   11880
   Top             =   390
   Width           =   12000
   Begin ComboBox cboYearsPay 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   10320
      TabIndex        =   1
      Text            =   "95 Years"
      Top             =   120
      Width           =   1455
   End
   Begin Frame fraSolveValues 
      Caption         =   "Special Illustration Options"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   3495
      Left            =   120
      TabIndex        =   49
      Top             =   4440
      Width           =   9375
      Begin TextBox txtBeginCashValue 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   7560
         MaxLength       =   10
         TabIndex        =   24
         Top             =   960
         Width           =   1575
      End
      Begin MaskEdBox txtBeginYear 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   5760
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   23
         Top             =   960
         Width           =   375
      End
      Begin TextBox txtSolvePrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   3240
         MaxLength       =   13
         TabIndex        =   51
         TabStop         =   0   'False
         Top             =   960
         Width           =   1455
      End
      Begin TextBox txtSolveCashValue 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   240
         MaxLength       =   10
         TabIndex        =   21
         Top             =   960
         Width           =   1455
      End
      Begin MaskEdBox txtSolveAge 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   2280
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   22
         Top             =   960
         Width           =   375
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "(Enter Begin At Age and Attained Cash Value, then press ILLUSTRATE to create an illustration showing projected values from that Attained (Begin) Age forward to lapse or endowment)"
         Height          =   1575
         Index           =   8
         Left            =   5880
         TabIndex        =   33
         Top             =   1560
         Width           =   3015
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Attained Cash Value   "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   7
         Left            =   7560
         TabIndex        =   34
         Top             =   720
         Width           =   1575
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Begin At Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   6
         Left            =   5400
         TabIndex        =   36
         Top             =   720
         Width           =   1095
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - Illustrate an Active Contract - - - - - - "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   5400
         TabIndex        =   56
         Top             =   360
         Width           =   3855
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "At Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   2160
         TabIndex        =   55
         Top             =   720
         Width           =   615
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "(Enter Cash Value and At Age, then press CALCULATE to return the level Modal Premium necessary to obtain that Target Cash Value at the desired Age.  For an illustration, put the returned Modal Premium amount into Planned Modal Premium above and press ILLUSTRATE)"
         Height          =   1575
         Index           =   4
         Left            =   480
         TabIndex        =   53
         Top             =   1560
         Width           =   3975
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Modal Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   3240
         TabIndex        =   54
         Top             =   720
         Width           =   1455
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Cash Value"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   240
         TabIndex        =   52
         Top             =   720
         Width           =   1455
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - - - - - Solve for Modal Premium - - - - - - - - - -"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   50
         Top             =   360
         Width           =   4695
      End
   End
   Begin TextBox txtPremiumLump 
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   10320
      MaxLength       =   10
      TabIndex        =   3
      Top             =   520
      Width           =   1455
   End
   Begin TextBox txtModalPremium 
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   3240
      MaxLength       =   10
      TabIndex        =   2
      Top             =   520
      Width           =   1455
   End
   Begin Frame fraCurrentInt 
      Height          =   1215
      Left            =   9600
      TabIndex        =   43
      Top             =   960
      Width           =   2175
      Begin TextBox txtPremCurrentInt 
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   600
         MaxLength       =   6
         TabIndex        =   15
         Top             =   720
         Width           =   1095
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "Current Interest Rate:"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   495
         Index           =   0
         Left            =   480
         TabIndex        =   35
         Top             =   120
         Width           =   1215
      End
   End
   Begin SSCommand cmdFuture 
      BevelWidth      =   4
      Caption         =   "&FUTURE Changes"
      Font3D          =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   9600
      TabIndex        =   19
      Top             =   6000
      Width           =   2175
   End
   Begin SSCommand cmdIllustrate 
      BevelWidth      =   4
      Caption         =   "&ILLUSTRATE"
      Font3D          =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   9600
      TabIndex        =   17
      Top             =   3480
      Width           =   2175
   End
   Begin SSCommand cmdRiders 
      BevelWidth      =   4
      Caption         =   "INSURED &RIDERS"
      Font3D          =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   9600
      TabIndex        =   18
      Top             =   4920
      Width           =   2175
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      Font3D          =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   9600
      TabIndex        =   20
      Top             =   7080
      Width           =   2175
   End
   Begin SSCommand cmdCalculate 
      BevelWidth      =   4
      Caption         =   "&CALCULATE"
      Font3D          =   0  'None
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   855
      Left            =   9600
      TabIndex        =   16
      Top             =   2400
      Width           =   2175
   End
   Begin Frame fraInForceYears 
      Height          =   735
      Left            =   120
      TabIndex        =   41
      Top             =   2160
      Width           =   9375
      Begin TextBox txtInForceYears 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   6720
         TabIndex        =   48
         TabStop         =   0   'False
         Text            =   " "
         Top             =   240
         Width           =   2415
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "Using these premium figures and the current interest rate, the policy will: "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   120
         TabIndex        =   42
         Top             =   240
         Width           =   6735
      End
   End
   Begin Frame fraCalcPremiums 
      Caption         =   "Calculated Premiums"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1335
      Left            =   120
      TabIndex        =   31
      Top             =   3000
      Width           =   9375
      Begin TextBox txtMaxSinglePrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   1
         Left            =   7680
         MaxLength       =   13
         TabIndex        =   57
         TabStop         =   0   'False
         Top             =   840
         Width           =   1455
      End
      Begin TextBox txtMaxSinglePrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Index           =   0
         Left            =   5760
         MaxLength       =   13
         TabIndex        =   47
         TabStop         =   0   'False
         Top             =   840
         Width           =   1455
      End
      Begin TextBox txtGuidePrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   3720
         MaxLength       =   13
         TabIndex        =   46
         TabStop         =   0   'False
         Top             =   840
         Width           =   1455
      End
      Begin TextBox txtTargPrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   1800
         MaxLength       =   13
         TabIndex        =   45
         TabStop         =   0   'False
         Top             =   840
         Width           =   1455
      End
      Begin TextBox txtMinPrem 
         BackColor       =   &H00C0C0C0&
         FontBold        =   -1  'True
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   360
         Left            =   120
         MaxLength       =   13
         TabIndex        =   44
         TabStop         =   0   'False
         Top             =   840
         Width           =   1335
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "(Including Spouse/ Additional Insureds)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   5
         Left            =   7560
         TabIndex        =   59
         Top             =   435
         Width           =   1695
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "(Principal Only)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   5760
         TabIndex        =   58
         Top             =   600
         Width           =   1455
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "- - - - - Maximum Guideline Single Premium - - - - -"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   5640
         TabIndex        =   40
         Top             =   240
         Width           =   3615
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Maximum Guideline Annual Premium (Principal Only)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Index           =   2
         Left            =   3720
         TabIndex        =   39
         Top             =   240
         Width           =   1455
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Target Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   2040
         TabIndex        =   38
         Top             =   360
         Width           =   975
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Minimum Modal Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   37
         Top             =   360
         Width           =   1335
      End
   End
   Begin Frame fraPremiumIndiv 
      Caption         =   "Select or Exclude Illustrated Insureds"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   1215
      Left            =   120
      TabIndex        =   32
      Top             =   960
      Width           =   9375
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Principal  (required)"
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   0
         Left            =   120
         TabIndex        =   25
         TabStop         =   0   'False
         Top             =   360
         Value           =   1  'Checked
         Width           =   2055
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Spouse Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   2
         Left            =   120
         TabIndex        =   5
         Top             =   840
         Width           =   1575
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Child Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   1
         Left            =   120
         TabIndex        =   4
         Top             =   600
         Width           =   1575
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #1"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   3
         Left            =   2760
         TabIndex        =   6
         Top             =   360
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #2"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   4
         Left            =   2760
         TabIndex        =   7
         Top             =   600
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #3"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   5
         Left            =   2760
         TabIndex        =   8
         Top             =   840
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #4"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   6
         Left            =   5040
         TabIndex        =   9
         Top             =   360
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #5"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   7
         Left            =   5040
         TabIndex        =   10
         Top             =   600
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #6"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   8
         Left            =   5040
         TabIndex        =   11
         Top             =   840
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #7"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   9
         Left            =   7440
         TabIndex        =   12
         Top             =   360
         Width           =   1335
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #8"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   10
         Left            =   7440
         TabIndex        =   13
         Top             =   600
         Width           =   1335
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #9"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   11
         Left            =   7440
         TabIndex        =   14
         Top             =   840
         Width           =   1335
      End
   End
   Begin ComboBox cboPremiumMode 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   360
      Left            =   1920
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   120
      Width           =   2775
   End
   Begin Label lblDefaultYears 
      Alignment       =   2  'Center
      Caption         =   "(Default: To Age 95)"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   8160
      TabIndex        =   30
      Top             =   150
      Width           =   1605
   End
   Begin Label lblPremiumYears 
      Caption         =   "Years to Pay Premium:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   5280
      TabIndex        =   29
      Top             =   120
      Width           =   2295
   End
   Begin Label lblModalPremium 
      Caption         =   "Planned Modal Premium:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   28
      Top             =   600
      Width           =   2775
   End
   Begin Label lblPremiumMode 
      Caption         =   "Premium Mode:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   120
      TabIndex        =   27
      Top             =   120
      Width           =   1455
   End
   Begin Label lblPremiumLump 
      Caption         =   "Lump Sum Initial Premium Amount:"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   5280
      TabIndex        =   26
      Top             =   600
      Width           =   3975
   End
End

Sub cboPremiumMode_GotFocus ()

    Need_Guideline = True
    Need_Ledger = True

End Sub

Sub cboPremiumMode_KeyDown (KeyCode As Integer, Shift As Integer)

   If KeyCode = KEY_RETURN Then cboYearsPay.SetFocus

End Sub

Sub cboYearsPay_Change ()

    Need_Ledger = True

End Sub

Sub cboYearsPay_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(cboYearsPay.Text)
    cboYearsPay.Text = ""
    cboYearsPay.ForeColor = &H80000005
    cboYearsPay.BackColor = &H80000008

End Sub

Sub cboYearsPay_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtModalPremium.SetFocus
    If KeyCode = KEY_ESCAPE Then
       cboYearsPay.Text = clipboard.GetText()
       txtModalPremium.SetFocus
    End If

End Sub

Sub cboYearsPay_LostFocus ()

    Dim TempAge As Integer

    TempAge = LSClient(SpouseClient).IssueAge
    
    Select Case cboYearsPay.Text
       Case "To Age 65"
          cboYearsPay.Text = Right$(Str$(65 - TempAge), 2) & " Years"
       Case "To Age 95"
          cboYearsPay.Text = Right$(Str$(95 - TempAge), 2) & " Years"
       Case "20 Years"
       Case Else
          If Val(cboYearsPay.Text) = 0 Then
             cboYearsPay.Text = Right$(Str$(95 - TempAge), 2) & " Years"
          Else
             cboYearsPay.Text = Left$(Trim$(cboYearsPay.Text), 2) & " Years"
          End If
    End Select

    If Val(cboYearsPay.Text) > 95 - TempAge Then
       msgline$ = "Years to pay premium must not exceed Client Attained Age 95"
       MsgBox msgline$
       cboYearsPay.Text = ""
       cboYearsPay.SetFocus
       Exit Sub
    End If

    If cboYearsPay.Text <> clipboard.GetText() Then txtModalPremium.SetFocus
    cboYearsPay.ForeColor = &H80000008
    cboYearsPay.BackColor = &H80000005

End Sub

Sub chkPremiumSelectIndiv_Click (Index As Integer)

    Select Case Index
       Case 1   'child
          If chkPremiumSelectIndiv(Index).Value = 0 Then
             LSClient(SpouseClient).ChildRider = False
          Else
             LSClient(SpouseClient).ChildRider = True
          End If
          Need_Ledger = True
       Case 2   'spouse
          If chkPremiumSelectIndiv(Index).Value = 0 Then
             LSClient(SpouseClient).SpouseRider = False
          Else
             LSClient(SpouseClient).SpouseRider = True
          End If
          Need_Ledger = True
       Case 3 To 11   'insureds
          If chkPremiumSelectIndiv(Index).Value = 0 Then
             LSClient(SpouseClient).InsuredRider(Index - 2) = False
          Else
             LSClient(SpouseClient).InsuredRider(Index - 2) = True
          End If
          Need_Ledger = True
       Case Else
    End Select

End Sub

Sub cmdCalculate_Click ()
    
    i = ReadyToIllustrate()
    If i = False Then
       msgtext$ = "Cannot Calculate until valid Age, Sex, Death Benefit and Interest Rate are input"
       MsgBox msgtext$
       Exit Sub
    End If

    Call Set_Premium_Illus_Parameters
    Call Calculate_Initial_Premiums

End Sub

Sub cmdExit_Click ()

    Call Set_Premium_Illus_Parameters
    Unload frmLSPREM
    If SpouseClient = 1 Then
       frmLSSPOUSE.Show 0
    Else
       frmLIFESV.Show 0
    End If

End Sub

Sub cmdFuture_Click ()
    
    Call Set_Premium_Illus_Parameters
    Unload frmLSPREM
    frmLSCHANGE.Show 0

End Sub

Sub cmdIllustrate_Click ()

    i = ReadyToIllustrate()
    If i = False Then
       msgtext$ = "Cannot Illustrate until valid Age, Sex, Death Benefit and Interest Rate are input"
       MsgBox msgtext$
       Exit Sub
    End If
    
    frmLSPREM.MousePointer = 11
    LifeIllusType = 1
    If Need_Guideline Or Need_Ledger Then
       Call Set_Premium_Illus_Parameters
    End If
    If Need_Guideline Then Call Calculate_Initial_Premiums
    If Need_Ledger Then Call Calculate_Full_LS_Ledger
    frmLSILLTYP.Show 0
    frmLSPREM.MousePointer = 0
    
End Sub

Sub cmdRiders_Click ()

    Call Set_Premium_Illus_Parameters
    Unload frmLSPREM
    frmLSINSD.Show 0

End Sub

Sub Form_Load ()
    
    If SpouseClient = 1 Then
       cmdExit.Caption = "E&XIT (to SPOUSE)"
       frmLSPREM.Caption = "Life Savings II: Premium Calculation for Spouse"
       For i% = 1 To 11
         chkPremiumSelectIndiv(i%).Visible = False
         chkPremiumSelectIndiv(i%).Value = 0
       Next i%
    Else
       cmdExit.Caption = "E&XIT (to CLIENT)"
       frmLSPREM.Caption = "Life Savings II: Premium Calculation for Client"
       If LSChildRider.FaceAmt > 0 Then
          chkPremiumSelectIndiv(1).Visible = True
          If LSClient(SpouseClient).ChildRider = True Then
             chkPremiumSelectIndiv(1).Value = 1
          End If
       Else
          chkPremiumSelectIndiv(1).Visible = False
          chkPremiumSelectIndiv(1).Value = 0
       End If
       If LSSpouseRider.FaceAmt > 0 Then
          chkPremiumSelectIndiv(2).Visible = True
          If LSClient(SpouseClient).SpouseRider = True Then
             chkPremiumSelectIndiv(2).Value = 1
          End If
       Else
          chkPremiumSelectIndiv(2).Visible = False
          chkPremiumSelectIndiv(2).Value = 0
       End If
       For i% = 3 To 11
         If LSInsuredRider(i% - 2).FaceAmt > 0 Then
            chkPremiumSelectIndiv(i%).Visible = True
            If LSClient(SpouseClient).InsuredRider(i% - 2) = True Then
               chkPremiumSelectIndiv(i%).Value = 1
            End If
         Else
            chkPremiumSelectIndiv(i%).Visible = False
            chkPremiumSelectIndiv(i%).Value = 0
         End If
       Next i%
    End If
    cboYearsPay.AddItem "20 Years"
    cboYearsPay.AddItem "To Age 65"
    cboYearsPay.AddItem "To Age 95"

    cboPremiumMode.AddItem "Monthly (A P P)"
    cboPremiumMode.AddItem "Quarterly"
    cboPremiumMode.AddItem "Semiannual"
    cboPremiumMode.AddItem "Annual"
    s% = SpouseClient
    Select Case LSClient(s%).Mode
       Case "A"
          cboPremiumMode.ListIndex = 3
       Case "S"
          cboPremiumMode.ListIndex = 2
       Case "Q"
          cboPremiumMode.ListIndex = 1
       Case Else
          cboPremiumMode.ListIndex = 0
    End Select
    If LSClient(s%).YearsToPay = 0 Then LSClient(s%).YearsToPay = 95 - LSClient(s%).IssueAge
    cboYearsPay.Text = Format$(LSClient(s%).YearsToPay, "#0") & " Years"
    txtModalPremium.Text = Format$(LSClient(s%).PlannedPrem, "#####00.00")
    txtPremiumLump.Text = Format$(LSClient(s%).LumpSum, "#####00.00")
    txtPremCurrentInt.Text = Format$(LSClient(s%).CurrRate, "00.000")

End Sub

Sub Form_Unload (Cancel As Integer)

    Need_Ledger = True
    Need_Guideline = True
    Need_Target_CV = True
    Set frmLSPREM = Nothing

End Sub

Sub txtBeginCashValue_Change ()

    Need_Ledger = True

End Sub

Sub txtBeginCashValue_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtBeginCashValue.Text)
    txtBeginCashValue.Text = ""
    txtBeginCashValue.Text = clipboard.GetText()
    txtBeginCashValue.ForeColor = &H80000005
    txtBeginCashValue.BackColor = &H80000008
 
End Sub

Sub txtBeginCashValue_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdCalculate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtBeginCashValue.Text = clipboard.GetText()
       cmdCalculate.SetFocus
    End If

End Sub

Sub txtBeginCashValue_KeyPress (KeyAscii As Integer)

    If KeyAscii <> 46 Then
       If (KeyAscii < 48 Or KeyAscii > 57) Then
          KeyAscii = 0
       End If
    End If

End Sub

Sub txtBeginCashValue_LostFocus ()
    
    Dim TempValue As Single

    txtBeginCashValue.ForeColor = &H80000008
    txtBeginCashValue.BackColor = &H80000005
    If Val(txtBeginCashValue.Text) > 0 And Val(txtBeginCashValue.Text) > 999999 Then
       msgline$ = "Attained Cash Value amount must be less than $1,000,000"
       MsgBox msgline$
       txtBeginCashValue.Text = ""
       txtBeginCashValue.SetFocus
       Exit Sub
    End If
    TempValue = Val(txtBeginCashValue.Text)
    If TempValue > 0 Then txtBeginCashValue.Text = Format$(TempValue, "######.00")

End Sub

Sub txtBeginYear_Change ()

    Need_Ledger = True

End Sub

Sub txtBeginYear_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtBeginYear.Text)
    txtBeginYear.Text = "__"
    txtBeginYear.Text = clipboard.GetText()
    txtBeginYear.ForeColor = &H80000005
    txtBeginYear.BackColor = &H80000008

End Sub

Sub txtBeginYear_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtBeginCashValue.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtBeginYear.Text = clipboard.GetText()
       txtBeginCashValue.SetFocus
    End If
 
End Sub

Sub txtBeginYear_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtBeginYear.Text = clipboard.GetText() Then
          txtBeginYear.Text = "__"
       End If
    End If
 
End Sub

Sub txtBeginYear_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtBeginYear.Text) > 9 Then
          Call txtBeginYear_LostFocus
       End If
    End If

End Sub

Sub txtBeginYear_LostFocus ()
    
    Dim TempAge As Integer
    Dim SvAge As Integer

    TempAge = LSClient(SpouseClient).IssueAge
    SvAge = Val(txtBeginYear.Text)
    If SvAge <> 0 And (SvAge < TempAge + 1 Or SvAge > 94) Then
       msgline$ = "Attained age for this calculation must be at least Client Age + 1 and less than age 94"
       MsgBox msgline$
       txtBeginYear.Text = "__"
       txtBeginYear.SetFocus
       Exit Sub
    End If

    txtBeginYear.ForeColor = &H80000008
    txtBeginYear.BackColor = &H80000005
    txtBeginCashValue.SetFocus

End Sub

Sub txtModalPremium_Change ()

    Need_Ledger = True

End Sub

Sub txtModalPremium_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtModalPremium.Text)
    txtModalPremium.Text = ""
    txtModalPremium.Text = clipboard.GetText()
    txtModalPremium.ForeColor = &H80000005
    txtModalPremium.BackColor = &H80000008

End Sub

Sub txtModalPremium_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtPremiumLump.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtModalPremium.Text = clipboard.GetText()
       txtPremiumLump.SetFocus
    End If

End Sub

Sub txtModalPremium_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtModalPremium_LostFocus ()
    
    Dim tempprem As Double

    txtModalPremium.ForeColor = &H80000008
    txtModalPremium.BackColor = &H80000005
    If Val(txtModalPremium.Text) > 999999 Then
       msgline$ = "Modal premium cannot exceed $999,999"
       MsgBox msgline$
       txtModalPremium.SetFocus
       Exit Sub
    End If
    tempprem = Val(txtModalPremium.Text)
    If tempprem > 0 Then txtModalPremium.Text = Format$(tempprem, "#####00.00")

End Sub

Sub txtPremCurrentInt_Change ()

    Need_Ledger = True
    Need_Guideline = True

End Sub

Sub txtPremCurrentInt_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtPremCurrentInt.Text)
    txtPremCurrentInt.Text = ""
    txtPremCurrentInt.Text = clipboard.GetText()
    txtPremCurrentInt.ForeColor = &H80000005
    txtPremCurrentInt.BackColor = &H80000008

End Sub

Sub txtPremCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdCalculate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPremCurrentInt.Text = clipboard.GetText()
       chkPremiumSelectIndiv(0).SetFocus
    End If

End Sub

Sub txtPremCurrentInt_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtPremCurrentInt_LostFocus ()
    
    Dim TempInt As Single

    txtPremCurrentInt.ForeColor = &H80000008
    txtPremCurrentInt.BackColor = &H80000005
    If Val(txtPremCurrentInt.Text) > 0 And (Val(txtPremCurrentInt.Text) < 4# Or Val(txtPremCurrentInt.Text) > 20#) Then
       msgline$ = "Current Interest Amount must be between 4.00% and 20.00%, inclusive"
       MsgBox msgline$
       txtPremCurrentInt.Text = ""
       txtPremCurrentInt.SetFocus
       Exit Sub
    End If
    TempInt = Val(txtPremCurrentInt.Text)
    If TempInt > 0 Then
       txtPremCurrentInt.Text = Format$(TempInt, "00.000")
       LSClient(SpouseClient).CurrRate = TempInt
    End If

End Sub

Sub txtPremiumLump_Change ()

    Need_Ledger = True

End Sub

Sub txtPremiumLump_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtPremiumLump.Text)
    txtPremiumLump.Text = ""
    txtPremiumLump.Text = clipboard.GetText()
    txtPremiumLump.ForeColor = &H80000005
    txtPremiumLump.BackColor = &H80000008

End Sub

Sub txtPremiumLump_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtPremCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPremiumLump.Text = clipboard.GetText()
       txtPremCurrentInt.SetFocus
    End If

End Sub

Sub txtPremiumLump_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtPremiumLump_LostFocus ()
    
    Dim tempprem As Double

    txtPremiumLump.ForeColor = &H80000008
    txtPremiumLump.BackColor = &H80000005
    If Val(txtPremiumLump.Text) > 9999999 Then
       msgline$ = "Lump Sum Amount cannot exceed $9,999,999"
       MsgBox msgline$
       txtPremiumLump.SetFocus
       Exit Sub
    End If
    tempprem = Val(txtPremiumLump.Text)
    If tempprem > 0 Then txtPremiumLump.Text = Format$(tempprem, "#####00.00")

End Sub

Sub txtSolveAge_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSolveAge.Text)
    txtSolveAge.Text = "__"
    txtSolveAge.Text = clipboard.GetText()
    txtSolveAge.ForeColor = &H80000005
    txtSolveAge.BackColor = &H80000008

End Sub

Sub txtSolveAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdCalculate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSolveAge.Text = clipboard.GetText()
       cmdCalculate.SetFocus
    End If

End Sub

Sub txtSolveAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtSolveAge.Text = clipboard.GetText() Then
          txtSolveAge.Text = "__"
       End If
    End If

End Sub

Sub txtSolveAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtSolveAge.Text) > 9 Then
          Call txtSolveAge_LostFocus
       End If
    End If

End Sub

Sub txtSolveAge_LostFocus ()
    
    Dim TempAge As Integer
    Dim SvAge As Integer

    TempAge = LSClient(SpouseClient).IssueAge
    SvAge = Val(txtSolveAge.Text)
    If SvAge <> 0 And (SvAge < TempAge + 1 Or SvAge > 95) Then
       msgline$ = "Attained age for this calculation must be at least Client Age + 1 and less than age 95"
       MsgBox msgline$
       txtSolveAge.Text = "__"
       txtSolveAge.SetFocus
       Exit Sub
    End If
    
    txtSolveAge.ForeColor = &H80000008
    txtSolveAge.BackColor = &H80000005
    Need_Target_CV = True
    cmdCalculate.SetFocus

End Sub

Sub txtSolveCashValue_GotFocus ()
    
    clipboard.Clear
    clipboard.SetText Trim$(txtSolveCashValue.Text)
    txtSolveCashValue.Text = ""
    txtSolveCashValue.Text = clipboard.GetText()
    txtSolveCashValue.ForeColor = &H80000005
    txtSolveCashValue.BackColor = &H80000008

End Sub

Sub txtSolveCashValue_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtSolveAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSolveCashValue.Text = clipboard.GetText()
       txtSolveAge.SetFocus
    End If

End Sub

Sub txtSolveCashValue_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) Then KeyAscii = 0

End Sub

Sub txtSolveCashValue_LostFocus ()
    
    Dim TempValue As Single

    If Val(txtSolveCashValue.Text) > 0 And Val(txtSolveCashValue.Text) > 9999999 Then
       msgline$ = "Cash Value amount must be less than $10,000,000"
       MsgBox msgline$
       txtSolveCashValue.Text = ""
       txtSolveCashValue.SetFocus
       Exit Sub
    End If
    TempValue = Val(txtSolveCashValue.Text)
    If TempValue > 0 Then txtSolveCashValue.Text = Format$(TempValue, "######0")

    txtSolveCashValue.ForeColor = &H80000008
    txtSolveCashValue.BackColor = &H80000005
    Need_Target_CV = True

End Sub

