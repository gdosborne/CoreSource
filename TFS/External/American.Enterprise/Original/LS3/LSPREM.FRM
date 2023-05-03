VERSION 2.00
Begin Form frmLSPREM 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "LifeSavings - Premium Calculation"
   ClientHeight    =   6870
   ClientLeft      =   90
   ClientTop       =   420
   ClientWidth     =   9555
   ClipControls    =   0   'False
   ControlBox      =   0   'False
   Height          =   7275
   Left            =   30
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6870
   ScaleWidth      =   9555
   Top             =   75
   Width           =   9675
   Begin ComboBox cboYearsPay 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   300
      Left            =   8160
      TabIndex        =   1
      Text            =   "95 Years"
      Top             =   120
      Width           =   1215
   End
   Begin Frame fraSolveValues 
      Caption         =   "Special Illustration Options"
      Height          =   2535
      Left            =   120
      TabIndex        =   48
      Top             =   4200
      Width           =   7335
      Begin TextBox txtCurrentDB 
         Alignment       =   1  'Right Justify
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   5880
         MaxLength       =   10
         TabIndex        =   67
         Top             =   1080
         Width           =   1335
      End
      Begin TextBox txtBeginLoan 
         Alignment       =   1  'Right Justify
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   5880
         MaxLength       =   10
         TabIndex        =   66
         Top             =   780
         Width           =   1335
      End
      Begin TextBox txtBeginCashValue 
         Alignment       =   1  'Right Justify
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   5880
         MaxLength       =   10
         TabIndex        =   23
         Top             =   480
         Width           =   1335
      End
      Begin TextBox txtSolvePrem 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   1800
         MaxLength       =   13
         TabIndex        =   50
         TabStop         =   0   'False
         Top             =   1080
         Width           =   1335
      End
      Begin TextBox txtSolveCashValue 
         Alignment       =   1  'Right Justify
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   1920
         MaxLength       =   10
         TabIndex        =   20
         Top             =   480
         Width           =   1215
      End
      Begin MaskEdBox txtBeginYear 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   4320
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   22
         Top             =   480
         Width           =   375
      End
      Begin MaskEdBox txtSolveAge 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   2760
         Mask            =   "##"
         MaxLength       =   2
         PromptChar      =   "_"
         TabIndex        =   21
         Top             =   780
         Width           =   375
      End
      Begin Label lblSolveValues 
         Caption         =   "Client Death Benefit at Begin Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   10
         Left            =   3720
         TabIndex        =   69
         Top             =   1125
         Width           =   2175
      End
      Begin Label lblSolveValues 
         Caption         =   "Loan Balance at Begin Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   9
         Left            =   3720
         TabIndex        =   68
         Top             =   845
         Width           =   1935
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Enter Begin at Age and Attained Cash Value, then press ILLUSTRATE to create an illustration showing projected values from that Attained (Begin) Age forward to lapse or endowment. (Enter Loan Balance at Begin Age or Client Death Benefit at Begin Age only if they have changed)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   975
         Index           =   8
         Left            =   3600
         TabIndex        =   32
         Top             =   1440
         Width           =   3615
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Attained Cash Value   "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   7
         Left            =   4800
         TabIndex        =   33
         Top             =   460
         Width           =   975
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Begin At Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   6
         Left            =   3680
         TabIndex        =   35
         Top             =   460
         Width           =   615
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - - - Illustrate an Active Contract - - - - - - - -"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   3600
         TabIndex        =   55
         Top             =   210
         Width           =   3615
      End
      Begin Label lblSolveValues 
         Caption         =   "At Age"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   480
         TabIndex        =   54
         Top             =   825
         Width           =   615
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "Enter Cash Value and At Age, then press CALCULATE to return the Level Modal Premium necessary to obtain that Target Cash Value at the desired Age.  For an illustration, put the returned Modal Premium amount into Planned Modal Premium (above) and press ILLUSTRATE. "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   975
         Index           =   4
         Left            =   120
         TabIndex        =   52
         Top             =   1440
         Width           =   3255
      End
      Begin Label lblSolveValues 
         Caption         =   "Modal Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   480
         TabIndex        =   53
         Top             =   1125
         Width           =   1335
      End
      Begin Label lblSolveValues 
         Caption         =   "Cash Value"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   0
         Left            =   480
         TabIndex        =   51
         Top             =   525
         Width           =   975
      End
      Begin Label lblSolveValues 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - Solve for Modal Premium - - - - - -"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   49
         Top             =   210
         Width           =   3255
      End
   End
   Begin TextBox txtPremiumLump 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   8160
      MaxLength       =   11
      TabIndex        =   3
      Top             =   480
      Width           =   1215
   End
   Begin TextBox txtModalPremium 
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   285
      Left            =   3240
      MaxLength       =   11
      TabIndex        =   2
      Top             =   480
      Width           =   1215
   End
   Begin Frame fraCurrentInt 
      Height          =   1215
      Left            =   7560
      TabIndex        =   42
      Top             =   780
      Width           =   1815
      Begin TextBox txtPremCurrentInt 
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   285
         Left            =   480
         MaxLength       =   6
         TabIndex        =   14
         Top             =   720
         Width           =   855
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "Current Interest Rate:"
         Height          =   495
         Index           =   0
         Left            =   240
         TabIndex        =   34
         Top             =   120
         Width           =   1215
      End
   End
   Begin Frame fraInForceYears 
      Caption         =   "Calculated Lapse/Endowment Information"
      Height          =   900
      Left            =   120
      TabIndex        =   40
      Top             =   2040
      Width           =   7335
      Begin TextBox txtEndowCurr 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   5760
         MaxLength       =   13
         TabIndex        =   64
         TabStop         =   0   'False
         Text            =   " "
         Top             =   570
         Width           =   1455
      End
      Begin TextBox txtEndowGuar 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   4200
         MaxLength       =   13
         TabIndex        =   63
         TabStop         =   0   'False
         Text            =   " "
         Top             =   570
         Width           =   1455
      End
      Begin TextBox txtInForceYears 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   1560
         TabIndex        =   47
         TabStop         =   0   'False
         Text            =   " "
         Top             =   570
         Width           =   2055
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "(Current Rates)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   5
         Left            =   5760
         TabIndex        =   65
         Top             =   360
         Width           =   1455
      End
      Begin Label lblInForceYears 
         Caption         =   "Using the above premium figures and the current interest"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   62
         Top             =   350
         Width           =   3735
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "(Guaranteed Rates)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   4
         Left            =   4200
         TabIndex        =   61
         Top             =   375
         Width           =   1455
      End
      Begin Label lblInForceYears 
         Alignment       =   2  'Center
         Caption         =   "- - - - - - - - - Cash Values at Age 95 - - - - - - - - - "
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   3
         Left            =   4200
         TabIndex        =   60
         Top             =   180
         Width           =   3015
      End
      Begin Label lblInForceYears 
         Caption         =   " rate, the policy will:"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   2
         Left            =   120
         TabIndex        =   41
         Top             =   570
         Width           =   1575
      End
   End
   Begin Frame fraCalcPremiums 
      Caption         =   "Calculated Premiums"
      Height          =   1215
      Left            =   120
      TabIndex        =   30
      Top             =   2950
      Width           =   7335
      Begin TextBox txtMaxSinglePrem 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Index           =   1
         Left            =   5880
         MaxLength       =   13
         TabIndex        =   56
         TabStop         =   0   'False
         Top             =   840
         Width           =   1215
      End
      Begin TextBox txtMaxSinglePrem 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Index           =   0
         Left            =   4440
         MaxLength       =   13
         TabIndex        =   46
         TabStop         =   0   'False
         Top             =   840
         Width           =   1215
      End
      Begin TextBox txtGuidePrem 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   3000
         MaxLength       =   13
         TabIndex        =   45
         TabStop         =   0   'False
         Top             =   840
         Width           =   1215
      End
      Begin TextBox txtTargPrem 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   1560
         MaxLength       =   13
         TabIndex        =   44
         TabStop         =   0   'False
         Top             =   840
         Width           =   1215
      End
      Begin TextBox txtMinPrem 
         BackColor       =   &H00C0C0C0&
         Height          =   285
         Left            =   120
         MaxLength       =   13
         TabIndex        =   43
         TabStop         =   0   'False
         Top             =   840
         Width           =   1215
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "(Including  Additional Insureds)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   5
         Left            =   5760
         TabIndex        =   58
         Top             =   480
         Width           =   1455
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "(Principal Only)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   4
         Left            =   4680
         TabIndex        =   57
         Top             =   480
         Width           =   735
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "--- Maximum Guideline Single Premium ---"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   252
         Index           =   3
         Left            =   4320
         TabIndex        =   39
         Top             =   240
         Width           =   2892
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Maximum Guideline Annual Premium (Principal Only)"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   615
         Index           =   2
         Left            =   2880
         TabIndex        =   38
         Top             =   240
         Width           =   1455
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Target Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   1
         Left            =   1680
         TabIndex        =   37
         Top             =   360
         Width           =   975
      End
      Begin Label lblCalcPrems 
         Alignment       =   2  'Center
         Caption         =   "Minimum Modal Premium"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "Small Fonts"
         FontSize        =   6.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   375
         Index           =   0
         Left            =   120
         TabIndex        =   36
         Top             =   360
         Width           =   1215
      End
   End
   Begin Frame fraPremiumIndiv 
      Caption         =   "Select or Exclude Illustrated Insureds"
      Height          =   1215
      Left            =   120
      TabIndex        =   31
      Top             =   780
      Width           =   7335
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Principal  (required)"
         Enabled         =   0   'False
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   0
         Left            =   120
         TabIndex        =   24
         TabStop         =   0   'False
         Top             =   360
         Value           =   1  'Checked
         Width           =   1935
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Child Rider"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
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
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   3
         Left            =   2160
         TabIndex        =   5
         Top             =   360
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #2"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   4
         Left            =   2160
         TabIndex        =   6
         Top             =   600
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #3"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   5
         Left            =   2160
         TabIndex        =   7
         Top             =   840
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #4"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   6
         Left            =   3840
         TabIndex        =   8
         Top             =   360
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #5"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   7
         Left            =   3840
         TabIndex        =   9
         Top             =   600
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #6"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   240
         Index           =   8
         Left            =   3840
         TabIndex        =   10
         Top             =   840
         Width           =   1455
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #7"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   9
         Left            =   5520
         TabIndex        =   11
         Top             =   360
         Width           =   1335
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #8"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   10
         Left            =   5520
         TabIndex        =   12
         Top             =   600
         Width           =   1335
      End
      Begin CheckBox chkPremiumSelectIndiv 
         Caption         =   "Insured #9"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   255
         Index           =   11
         Left            =   5520
         TabIndex        =   13
         Top             =   840
         Width           =   1335
      End
   End
   Begin ComboBox cboPremiumMode 
      Height          =   300
      Left            =   1920
      Style           =   2  'Dropdown List
      TabIndex        =   0
      Top             =   120
      Width           =   2535
   End
   Begin SSCommand cmdFuture 
      BevelWidth      =   4
      Caption         =   "&FUTURE Changes"
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7560
      TabIndex        =   18
      Top             =   5160
      Width           =   1815
   End
   Begin SSCommand cmdIllustrate 
      BevelWidth      =   4
      Caption         =   "&ILLUSTRATE"
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7560
      TabIndex        =   16
      Top             =   3000
      Width           =   1815
   End
   Begin SSCommand cmdRiders 
      BevelWidth      =   4
      Caption         =   "INSURED &RIDERS"
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7560
      TabIndex        =   17
      Top             =   4320
      Width           =   1815
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7560
      TabIndex        =   19
      Top             =   6000
      Width           =   1815
   End
   Begin SSCommand cmdCalculate 
      BevelWidth      =   4
      Caption         =   "&CALCULATE"
      ForeColor       =   &H00000000&
      Height          =   735
      Left            =   7560
      TabIndex        =   15
      Top             =   2160
      Width           =   1815
   End
   Begin Label lblButtonHead 
      Alignment       =   2  'Center
      Caption         =   "Jump to other displays using buttons (below):"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   8.25
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   400
      Left            =   7560
      TabIndex        =   59
      Top             =   3880
      Width           =   1815
   End
   Begin Label lblDefaultYears 
      Alignment       =   2  'Center
      Caption         =   "(Default: To Age 95)"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "Small Fonts"
      FontSize        =   6.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   255
      Left            =   6840
      TabIndex        =   29
      Top             =   150
      Width           =   1245
   End
   Begin Label lblPremiumYears 
      Caption         =   "Years to Pay Premium:"
      Height          =   255
      Left            =   4800
      TabIndex        =   28
      Top             =   120
      Width           =   1935
   End
   Begin Label lblModalPremium 
      Caption         =   "Planned Modal Premium:"
      Height          =   255
      Left            =   120
      TabIndex        =   27
      Top             =   480
      Width           =   2775
   End
   Begin Label lblPremiumMode 
      Caption         =   "Premium Mode:"
      Height          =   255
      Left            =   120
      TabIndex        =   26
      Top             =   120
      Width           =   1455
   End
   Begin Label lblPremiumLump 
      Caption         =   "Lump Sum Initial Premium Amount:"
      Height          =   255
      Left            =   4800
      TabIndex        =   25
      Top             =   480
      Width           =   3015
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
    
    Clipboard.Clear
    Clipboard.SetText Trim$(cboYearsPay.Text)
    cboYearsPay.Text = ""
    cboYearsPay.ForeColor = &H80000005
    cboYearsPay.BackColor = &H80000008

End Sub

Sub cboYearsPay_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtModalPremium.SetFocus
    If KeyCode = KEY_ESCAPE Then
       cboYearsPay.Text = Clipboard.GetText()
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
       MsgText = "Years to pay premium must not exceed Client Attained Age 95"
       MsgBox MsgText, , "LifeSavings"
       cboYearsPay.Text = ""
       cboYearsPay.SetFocus
       Exit Sub
    End If

    If cboYearsPay.Text <> Clipboard.GetText() Then txtModalPremium.SetFocus
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
    If i <> 0 Then
       Select Case (i)
          Case 1
             MsgText = "Cannot calculate until client issue age is valid."
          Case 2
             MsgText = "Cannot calculate until client gender is valid."
          Case 3
             MsgText = "Cannot calculate until client face amount is valid."
          Case 4
             MsgText = "Cannot calculate until client face amount is valid for state."
          Case 5
             MsgText = "Cannot calculate until client interest rate is valid."
          Case Else
       End Select
       MsgBox MsgText, , "LifeSavings"
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
    If i <> 0 Then
       Select Case (i)
          Case 1
             MsgText = "Cannot illustrate until client issue age is valid."
          Case 2
             MsgText = "Cannot illustrate until client gender is valid."
          Case 3
             MsgText = "Cannot illustrate until client face amount is valid."
          Case 4
             MsgText = "Cannot illustrate until client face amount is valid for state."
          Case 5
             MsgText = "Cannot illustrate until client interest rate is valid."
          Case Else
       End Select
       MsgBox MsgText, , "LifeSavings"
       Exit Sub
    End If
    
    frmLSPREM.MousePointer = 11
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
       frmLSPREM.Caption = "LifeSavings: Premium Calculation for Spouse"
       For i% = 1 To 11
         If i% <> 2 Then     '2 was Spouse on LifeSavings 2
            chkPremiumSelectIndiv(i%).Visible = False
            chkPremiumSelectIndiv(i%).Value = 0
         End If
       Next i%
    Else
       cmdExit.Caption = "E&XIT (to CLIENT)"
       frmLSPREM.Caption = "LifeSavings: Premium Calculation for Client"
       If LSClient(SpouseClient).ChildRider = True Then
          chkPremiumSelectIndiv(1).Visible = True
          If LSClient(SpouseClient).ChildRider = True Then
             chkPremiumSelectIndiv(1).Value = 1
          End If
       Else
          chkPremiumSelectIndiv(1).Visible = False
          chkPremiumSelectIndiv(1).Value = 0
       End If
       
       For i% = 3 To 11
         If LSClient(SpouseClient).InsuredRider(i% - 2) = True Then
            chkPremiumSelectIndiv(i%).Visible = True
            chkPremiumSelectIndiv(i%).Value = 1
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
    txtPremCurrentInt.Text = Format$(LSClient(s%).CurrIntRate, "00.000")
    txtBeginLoan.Text = Format$(0, "#####0.00")
    txtCurrentDB.Text = Format$(LSClient(s%).FaceAmt, "##00000")

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
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtBeginCashValue.Text)
    txtBeginCashValue.Text = ""
    txtBeginCashValue.Text = Clipboard.GetText()
    txtBeginCashValue.ForeColor = &H80000005
    txtBeginCashValue.BackColor = &H80000008
 
End Sub

Sub txtBeginCashValue_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtBeginLoan.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtBeginCashValue.Text = Clipboard.GetText()
       cmdIllustrate.SetFocus
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
       MsgText = "Attained Cash Value amount must be less than $1,000,000"
       MsgBox MsgText, , "LifeSavings"
       txtBeginCashValue.Text = ""
       txtBeginCashValue.SetFocus
       Exit Sub
    End If
    TempValue = Val(txtBeginCashValue.Text)
    If TempValue > 0 Then txtBeginCashValue.Text = Format$(TempValue, "######.00")

End Sub

Sub txtBeginLoan_Change ()

    Need_Ledger = True

End Sub

Sub txtBeginLoan_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtBeginLoan.Text)
    txtBeginLoan.Text = ""
    txtBeginLoan.Text = Clipboard.GetText()
    txtBeginLoan.ForeColor = &H80000005
    txtBeginLoan.BackColor = &H80000008

End Sub

Sub txtBeginLoan_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtCurrentDB.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtBeginLoan.Text = Clipboard.GetText()
       cmdIllustrate.SetFocus
    End If

End Sub

Sub txtBeginLoan_KeyPress (KeyAscii As Integer)

    If KeyAscii <> 46 Then
       If (KeyAscii < 48 Or KeyAscii > 57) Then
          KeyAscii = 0
       End If
    End If

End Sub

Sub txtBeginLoan_LostFocus ()
    
    Dim TempValue As Single

    txtBeginLoan.ForeColor = &H80000008
    txtBeginLoan.BackColor = &H80000005
    If Val(txtBeginLoan.Text) > 0 And Val(txtBeginLoan.Text) > 999999 Then
       MsgText = "Attained Loan Value amount must be less than $1,000,000"
       MsgBox MsgText, , "LifeSavings"
       txtBeginLoan.Text = ""
       txtBeginLoan.SetFocus
       Exit Sub
    End If
    TempValue = Val(txtBeginLoan.Text)
    If TempValue > 0 Then txtBeginLoan.Text = Format$(TempValue, "######.00")

End Sub

Sub txtBeginYear_Change ()

    Need_Ledger = True

End Sub

Sub txtBeginYear_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtBeginYear.Text)
    txtBeginYear.Text = "__"
    txtBeginYear.Text = Clipboard.GetText()
    txtBeginYear.ForeColor = &H80000005
    txtBeginYear.BackColor = &H80000008

End Sub

Sub txtBeginYear_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtBeginCashValue.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtBeginYear.Text = Clipboard.GetText()
       txtBeginCashValue.SetFocus
    End If
 
End Sub

Sub txtBeginYear_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtBeginYear.Text = Clipboard.GetText() Then
          txtBeginYear.Text = "__"
       End If
    End If
 
End Sub

Sub txtBeginYear_KeyUp (KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
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
       MsgText = "Attained age for this calculation must be at least Client Age + 1 and less than age 95"
       MsgBox MsgText, , "LifeSavings"
       txtBeginYear.Text = "__"
       txtBeginYear.SetFocus
       Exit Sub
    End If

    txtBeginYear.ForeColor = &H80000008
    txtBeginYear.BackColor = &H80000005
    txtBeginCashValue.SetFocus

End Sub

Sub txtCurrentDB_Change ()

    Need_Ledger = True

End Sub

Sub txtCurrentDB_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtCurrentDB.Text)
    txtCurrentDB.Text = ""
    txtCurrentDB.Text = Clipboard.GetText()
    txtCurrentDB.ForeColor = &H80000005
    txtCurrentDB.BackColor = &H80000008

End Sub

Sub txtCurrentDB_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdIllustrate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtCurrentDB.Text = Clipboard.GetText()
       cmdIllustrate.SetFocus
    End If

End Sub

Sub txtCurrentDB_KeyPress (KeyAscii As Integer)

    If KeyAscii <> 46 Then
       If (KeyAscii < 48 Or KeyAscii > 57) Then
          KeyAscii = 0
       End If
    End If

End Sub

Sub txtCurrentDB_LostFocus ()
    
    Dim TempValue As Single

    txtCurrentDB.ForeColor = &H80000008
    txtCurrentDB.BackColor = &H80000005
    If Val(txtCurrentDB.Text) < 15000 Or Val(txtCurrentDB.Text) > 5000000 Then
       MsgText = "Current Client Death Benefit amount must be between $15,000 and $5,000,000"
       MsgBox MsgText, , "LifeSavings"
       txtCurrentDB.Text = ""
       txtCurrentDB.SetFocus
       Exit Sub
    End If
    TempValue = Val(txtCurrentDB.Text)
    If TempValue > 0 Then txtCurrentDB.Text = Format$(TempValue, "######.00")

End Sub

Sub txtModalPremium_Change ()

    Need_Ledger = True

End Sub

Sub txtModalPremium_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtModalPremium.Text)
    txtModalPremium.Text = ""
    txtModalPremium.Text = Clipboard.GetText()
    txtModalPremium.ForeColor = &H80000005
    txtModalPremium.BackColor = &H80000008

End Sub

Sub txtModalPremium_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtPremiumLump.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtModalPremium.Text = Clipboard.GetText()
       txtPremiumLump.SetFocus
    End If

End Sub

Sub txtModalPremium_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtModalPremium_LostFocus ()
    
    Dim TempPrem As Double

    txtModalPremium.ForeColor = &H80000008
    txtModalPremium.BackColor = &H80000005
    If Val(txtModalPremium.Text) > 9999999 Then
       MsgText = "Modal premium cannot exceed $9,999,999"
       MsgBox MsgText, , "LifeSavings"
       txtModalPremium.SetFocus
       Exit Sub
    End If
    TempPrem = Val(txtModalPremium.Text)
    If TempPrem > 0 Then txtModalPremium.Text = Format$(TempPrem, "#####00.00")

End Sub

Sub txtPremCurrentInt_Change ()

    Need_Ledger = True
    Need_Guideline = True

End Sub

Sub txtPremCurrentInt_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtPremCurrentInt.Text)
    txtPremCurrentInt.Text = ""
    txtPremCurrentInt.Text = Clipboard.GetText()
    txtPremCurrentInt.ForeColor = &H80000005
    txtPremCurrentInt.BackColor = &H80000008

End Sub

Sub txtPremCurrentInt_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdCalculate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPremCurrentInt.Text = Clipboard.GetText()
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
    If Val(txtPremCurrentInt.Text) > 0 And (Val(txtPremCurrentInt.Text) < 3# Or Val(txtPremCurrentInt.Text) > 20#) Then
       MsgText = "Current Interest Rate must be between 3.00% and the Current Interest Rate.  Do not use an Interest Rate that is greater than the Current Rate."
       MsgBox MsgText, , "LifeSavings"
       txtPremCurrentInt.Text = ""
       txtPremCurrentInt.SetFocus
       Exit Sub
    End If
    TempInt = Val(txtPremCurrentInt.Text)
    If TempInt > 0 Then
       txtPremCurrentInt.Text = Format$(TempInt, "00.000")
       LSClient(SpouseClient).CurrIntRate = TempInt
    End If

End Sub

Sub txtPremiumLump_Change ()

    Need_Ledger = True

End Sub

Sub txtPremiumLump_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtPremiumLump.Text)
    txtPremiumLump.Text = ""
    txtPremiumLump.Text = Clipboard.GetText()
    txtPremiumLump.ForeColor = &H80000005
    txtPremiumLump.BackColor = &H80000008

End Sub

Sub txtPremiumLump_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtPremCurrentInt.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtPremiumLump.Text = Clipboard.GetText()
       txtPremCurrentInt.SetFocus
    End If

End Sub

Sub txtPremiumLump_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtPremiumLump_LostFocus ()
    
    Dim TempPrem As Double

    txtPremiumLump.ForeColor = &H80000008
    txtPremiumLump.BackColor = &H80000005
    If Val(txtPremiumLump.Text) > 99999999 Then
       MsgText = "Lump Sum Amount cannot exceed $99,999,999."
       MsgBox MsgText, , "LifeSavings"
       txtPremiumLump.SetFocus
       Exit Sub
    End If
    TempPrem = Val(txtPremiumLump.Text)
    If TempPrem > 0 Then txtPremiumLump.Text = Format$(TempPrem, "#####00.00")

End Sub

Sub txtSolveAge_GotFocus ()
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSolveAge.Text)
    txtSolveAge.Text = "__"
    txtSolveAge.Text = Clipboard.GetText()
    txtSolveAge.ForeColor = &H80000005
    txtSolveAge.BackColor = &H80000008

End Sub

Sub txtSolveAge_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then cmdCalculate.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSolveAge.Text = Clipboard.GetText()
       cmdCalculate.SetFocus
    End If

End Sub

Sub txtSolveAge_KeyPress (KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtSolveAge.Text = Clipboard.GetText() Then
          txtSolveAge.Text = "__"
       End If
    End If

End Sub

Sub txtSolveAge_KeyUp (KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
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
       MsgText = "Attained age for this calculation must be at least Client Age + 1 and less than age 95"
       MsgBox MsgText, , "LifeSavings"
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
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtSolveCashValue.Text)
    txtSolveCashValue.Text = ""
    txtSolveCashValue.Text = Clipboard.GetText()
    txtSolveCashValue.ForeColor = &H80000005
    txtSolveCashValue.BackColor = &H80000008

End Sub

Sub txtSolveCashValue_KeyDown (KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then cboPremiumMode.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdIllustrate.SetFocus
    If KeyCode = KEY_RETURN Then txtSolveAge.SetFocus
    If KeyCode = KEY_ESCAPE Then
       txtSolveCashValue.Text = Clipboard.GetText()
       txtSolveAge.SetFocus
    End If

End Sub

Sub txtSolveCashValue_KeyPress (KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) Then KeyAscii = 0

End Sub

Sub txtSolveCashValue_LostFocus ()
    
    Dim TempValue As Single

    If Val(txtSolveCashValue.Text) > 0 And Val(txtSolveCashValue.Text) > 9999999 Then
       MsgText = "Cash Value amount must be less than $10,000,000"
       MsgBox MsgText, , "LifeSavings"
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

