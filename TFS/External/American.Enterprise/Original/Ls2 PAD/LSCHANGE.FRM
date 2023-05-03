VERSION 2.00
Begin Form frmLSCHANGE 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Life Savings II - Changes in Future Years"
   ClientHeight    =   8025
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   11880
   ControlBox      =   0   'False
   Height          =   8505
   Left            =   0
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   8025
   ScaleWidth      =   11880
   Top             =   0
   Width           =   12000
   Begin SSCommand cmdRiders 
      BevelWidth      =   4
      Caption         =   "INSURED &RIDERS"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   7680
      TabIndex        =   94
      Top             =   7200
      Width           =   2415
   End
   Begin SSCommand cmdPrem 
      BevelWidth      =   4
      Caption         =   "&PREMIUM CALC"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   4440
      TabIndex        =   93
      Top             =   7200
      Width           =   2415
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      FontBold        =   -1  'True
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   735
      Left            =   1200
      TabIndex        =   92
      Top             =   7200
      Width           =   2415
   End
   Begin Frame fraWDandLoan 
      Caption         =   "Withdrawals, Loans and Loan Repayments"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   3495
      Left            =   120
      TabIndex        =   101
      Top             =   3600
      Width           =   11655
      Begin CheckBox chkLoanRepay 
         Caption         =   "Annual Loan Repayments"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   8400
         TabIndex        =   76
         Top             =   240
         Width           =   3135
      End
      Begin CheckBox chkLoan 
         Caption         =   "Annual Policy Loan"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   4200
         TabIndex        =   60
         Top             =   240
         Width           =   3135
      End
      Begin Frame fraLoanRepay 
         Height          =   2895
         Left            =   8400
         TabIndex        =   96
         Top             =   480
         Width           =   3135
         Begin TextBox txtLoanRepay 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   90
            Top             =   2400
            Width           =   1455
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   87
            Top             =   1920
            Width           =   1455
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   84
            Top             =   1440
            Width           =   1455
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   81
            Top             =   960
            Width           =   1455
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   91
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   88
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   85
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   82
            Top             =   960
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   89
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   86
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   83
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   80
            Top             =   960
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   79
            Top             =   480
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   77
            Top             =   480
            Width           =   375
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   78
            Top             =   480
            Width           =   1455
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   5
            Left            =   2520
            TabIndex        =   121
            Top             =   280
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   4
            Left            =   720
            TabIndex        =   129
            Top             =   280
            Width           =   1455
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   3
            Left            =   120
            TabIndex        =   128
            Top             =   280
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   2
            Left            =   2520
            TabIndex        =   115
            Top             =   120
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Annual Repay"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   1
            Left            =   720
            TabIndex        =   116
            Top             =   100
            Width           =   1455
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   0
            Left            =   120
            TabIndex        =   97
            Top             =   100
            Width           =   375
         End
      End
      Begin Frame fraLoans 
         Height          =   2895
         Left            =   4200
         TabIndex        =   98
         Top             =   480
         Width           =   3135
         Begin TextBox txtLoan 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   74
            Top             =   2400
            Width           =   1455
         End
         Begin TextBox txtLoan 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   71
            Top             =   1920
            Width           =   1455
         End
         Begin TextBox txtLoan 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   68
            Top             =   1440
            Width           =   1455
         End
         Begin TextBox txtLoan 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   65
            Top             =   960
            Width           =   1455
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   75
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   72
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   69
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   66
            Top             =   960
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   73
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   70
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   67
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   64
            Top             =   960
            Width           =   375
         End
         Begin TextBox txtLoan 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   62
            Top             =   480
            Width           =   1455
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   61
            Top             =   480
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   63
            Top             =   480
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   5
            Left            =   2520
            TabIndex        =   127
            Top             =   280
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   4
            Left            =   720
            TabIndex        =   126
            Top             =   280
            Width           =   1455
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   3
            Left            =   120
            TabIndex        =   125
            Top             =   280
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   2
            Left            =   2520
            TabIndex        =   114
            Top             =   100
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Annual Loan"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   1
            Left            =   720
            TabIndex        =   113
            Top             =   100
            Width           =   1455
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   0
            Left            =   120
            TabIndex        =   99
            Top             =   100
            Width           =   375
         End
      End
      Begin CheckBox chkWithdraw 
         Caption         =   "Withdrawals"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   240
         TabIndex        =   44
         Top             =   240
         Width           =   3135
      End
      Begin Frame fraWithdraw 
         Height          =   2895
         Left            =   240
         TabIndex        =   100
         Top             =   480
         Width           =   3135
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   59
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   56
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   53
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   50
            Top             =   960
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   2520
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   47
            Top             =   480
            Width           =   375
         End
         Begin TextBox txtWithdraw 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   58
            Top             =   2400
            Width           =   1455
         End
         Begin TextBox txtWithdraw 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   55
            Top             =   1920
            Width           =   1455
         End
         Begin TextBox txtWithdraw 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   52
            Top             =   1440
            Width           =   1455
         End
         Begin TextBox txtWithdraw 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   49
            Top             =   960
            Width           =   1455
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   57
            Top             =   2400
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   54
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   51
            Top             =   1440
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   48
            Top             =   960
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   45
            Top             =   480
            Width           =   375
         End
         Begin TextBox txtWithdraw 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   46
            Top             =   480
            Width           =   1455
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   5
            Left            =   2520
            TabIndex        =   124
            Top             =   280
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   4
            Left            =   720
            TabIndex        =   123
            Top             =   280
            Width           =   1455
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   3
            Left            =   120
            TabIndex        =   122
            Top             =   280
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   2
            Left            =   2520
            TabIndex        =   112
            Top             =   100
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Withdrawal"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   1
            Left            =   720
            TabIndex        =   111
            Top             =   100
            Width           =   1455
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   235
            Index           =   0
            Left            =   120
            TabIndex        =   102
            Top             =   100
            Width           =   375
         End
      End
   End
   Begin Frame fraChange 
      Caption         =   "Death Benefits, Premiums, Interest Rates and Options"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   3375
      Left            =   120
      TabIndex        =   95
      Top             =   120
      Width           =   11655
      Begin CheckBox chkChangeDBOpt 
         Caption         =   "Death Benefit Option"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   9360
         TabIndex        =   33
         Top             =   240
         Width           =   2175
      End
      Begin Frame fraChangeDBOpt 
         Height          =   2775
         Left            =   9360
         TabIndex        =   103
         Top             =   480
         Width           =   2175
         Begin MaskEdBox txtChangeDBOptAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   42
            Top             =   2280
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOptAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   40
            Top             =   1800
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOptAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   38
            Top             =   1320
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOptAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   36
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOpt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   1080
            Mask            =   "#"
            MaxLength       =   1
            PromptChar      =   "_"
            TabIndex        =   43
            Top             =   2280
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOpt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   1080
            Mask            =   "#"
            MaxLength       =   1
            PromptChar      =   "_"
            TabIndex        =   41
            Top             =   1800
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOpt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   1080
            Mask            =   "#"
            MaxLength       =   1
            PromptChar      =   "_"
            TabIndex        =   39
            Top             =   1320
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOpt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   1080
            Mask            =   "#"
            MaxLength       =   1
            PromptChar      =   "_"
            TabIndex        =   37
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOpt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   1080
            Mask            =   "#"
            MaxLength       =   1
            PromptChar      =   "_"
            TabIndex        =   35
            Top             =   360
            Width           =   375
         End
         Begin MaskEdBox txtChangeDBOptAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   34
            Top             =   360
            Width           =   375
         End
         Begin Label lblChangeDBOpt 
            Alignment       =   2  'Center
            Caption         =   "New Option"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   720
            TabIndex        =   120
            Top             =   120
            Width           =   1095
         End
         Begin Label lblChangeDBOpt 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   104
            Top             =   120
            Width           =   375
         End
      End
      Begin CheckBox chkChangeCurrInt 
         Caption         =   "Current Interest Rate"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   6360
         TabIndex        =   22
         Top             =   240
         Width           =   2175
      End
      Begin Frame fraChangeCurrInt 
         Height          =   2775
         Left            =   6360
         TabIndex        =   110
         Top             =   480
         Width           =   2175
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   31
            Top             =   2280
            Width           =   375
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   29
            Top             =   1800
            Width           =   375
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   27
            Top             =   1320
            Width           =   375
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   25
            Top             =   840
            Width           =   375
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   840
            MaxLength       =   6
            TabIndex        =   32
            Top             =   2280
            Width           =   975
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   840
            MaxLength       =   6
            TabIndex        =   30
            Top             =   1800
            Width           =   975
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   840
            MaxLength       =   6
            TabIndex        =   28
            Top             =   1320
            Width           =   975
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   840
            MaxLength       =   6
            TabIndex        =   26
            Top             =   840
            Width           =   975
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   840
            MaxLength       =   6
            TabIndex        =   24
            Top             =   360
            Width           =   975
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   23
            Top             =   360
            Width           =   375
         End
         Begin Label lblChangeCurrInt 
            Alignment       =   2  'Center
            Caption         =   "New Interest Rate"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   600
            TabIndex        =   119
            Top             =   120
            Width           =   1455
         End
         Begin Label lblChangeCurrInt 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   105
            Top             =   120
            Width           =   375
         End
      End
      Begin CheckBox chkChangePrem 
         Caption         =   "Modal Premium Amount"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   3120
         TabIndex        =   11
         Top             =   240
         Width           =   2415
      End
      Begin Frame fraChangePrem 
         Height          =   2775
         Left            =   3240
         TabIndex        =   108
         Top             =   480
         Width           =   2175
         Begin TextBox txtChangePrem 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   21
            Top             =   2280
            Width           =   1215
         End
         Begin TextBox txtChangePrem 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   19
            Top             =   1800
            Width           =   1215
         End
         Begin TextBox txtChangePrem 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   17
            Top             =   1320
            Width           =   1215
         End
         Begin TextBox txtChangePrem 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   15
            Top             =   840
            Width           =   1215
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   20
            Top             =   2280
            Width           =   375
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   18
            Top             =   1800
            Width           =   375
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   16
            Top             =   1320
            Width           =   375
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   14
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   12
            Top             =   360
            Width           =   375
         End
         Begin TextBox txtChangePrem 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   13
            Top             =   360
            Width           =   1215
         End
         Begin Label lblChangePrem 
            Alignment       =   2  'Center
            Caption         =   "New Premium"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   720
            TabIndex        =   118
            Top             =   120
            Width           =   1215
         End
         Begin Label lblChangePrem 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   109
            Top             =   120
            Width           =   375
         End
      End
      Begin CheckBox chkChangeFace 
         Caption         =   "Specified Death Benefit"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   9.75
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   120
         TabIndex        =   0
         Top             =   240
         Width           =   2415
      End
      Begin Frame fraChangeFace 
         Height          =   2775
         Left            =   240
         TabIndex        =   106
         Top             =   480
         Width           =   2175
         Begin TextBox txtChangeFace 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   600
            MaxLength       =   8
            TabIndex        =   10
            Top             =   2280
            Width           =   1335
         End
         Begin TextBox txtChangeFace 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   600
            MaxLength       =   8
            TabIndex        =   8
            Top             =   1800
            Width           =   1335
         End
         Begin TextBox txtChangeFace 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   600
            MaxLength       =   8
            TabIndex        =   6
            Top             =   1320
            Width           =   1335
         End
         Begin TextBox txtChangeFace 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   600
            MaxLength       =   8
            TabIndex        =   4
            Top             =   840
            Width           =   1335
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   9
            Top             =   2280
            Width           =   375
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   7
            Top             =   1800
            Width           =   375
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   5
            Top             =   1320
            Width           =   375
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   3
            Top             =   840
            Width           =   375
         End
         Begin TextBox txtChangeFace 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   600
            MaxLength       =   8
            TabIndex        =   2
            Top             =   360
            Width           =   1335
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   9.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   360
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   1
            Top             =   360
            Width           =   375
         End
         Begin Label lblChangeFace 
            Alignment       =   2  'Center
            Caption         =   "New Benefit"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   600
            TabIndex        =   117
            Top             =   120
            Width           =   1335
         End
         Begin Label lblChangeFace 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   120
            TabIndex        =   107
            Top             =   120
            Width           =   375
         End
      End
   End
End

Sub chkChangeCurrInt_Click ()

    lblChangeCurrInt(0).Visible = chkChangeCurrInt.Value
    lblChangeCurrInt(1).Visible = chkChangeCurrInt.Value
    For i% = 0 To 4
      txtChangeCurrIntAge(i%).Visible = chkChangeCurrInt.Value
      txtChangeCurrInt(i%).Visible = chkChangeCurrInt.Value
      If chkChangeCurrInt.Value = 0 Then
         txtChangeCurrIntAge(i%).Text = "__"
         txtChangeCurrInt(i%).Text = ""
      End If
    Next i%
    
End Sub

Sub chkChangeCurrInt_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkChangeCurrInt_Click

End Sub

Sub chkChangeDBOpt_Click ()

    lblChangeDBOpt(0).Visible = chkChangeDBOpt.Value
    lblChangeDBOpt(1).Visible = chkChangeDBOpt.Value
    For i% = 0 To 4
      txtChangeDBOptAge(i%).Visible = chkChangeDBOpt.Value
      txtChangeDBOpt(i%).Visible = chkChangeDBOpt.Value
      If chkChangeDBOpt.Value = 0 Then
         txtChangeDBOptAge(i%).Text = "__"
         txtChangeDBOpt(i%).Text = "_"
      End If
    Next i%

End Sub

Sub chkChangeDBOpt_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkChangeDBOpt_Click

End Sub

Sub chkChangeFace_Click ()

    lblChangeFace(0).Visible = chkChangeFace.Value
    lblChangeFace(1).Visible = chkChangeFace.Value
    For i% = 0 To 4
      txtChangeFaceAge(i%).Visible = chkChangeFace.Value
      txtChangeFace(i%).Visible = chkChangeFace.Value
      If chkChangeFace.Value = 0 Then
         txtChangeFaceAge(i%).Text = "__"
         txtChangeFace(i%).Text = ""
      End If
    Next i%

End Sub

Sub chkChangeFace_KeyDown (KeyCode As Integer, Shift As Integer)
 
    Call chkChangeFace_Click

End Sub

Sub chkChangePrem_Click ()

    lblChangePrem(0).Visible = chkChangePrem.Value
    lblChangePrem(1).Visible = chkChangePrem.Value
    For i% = 0 To 4
      txtChangePremAge(i%).Visible = chkChangePrem.Value
      txtChangePrem(i%).Visible = chkChangePrem.Value
      If chkChangePrem.Value = 0 Then
         txtChangePremAge(i%).Text = "__"
         txtChangePrem(i%).Text = ""
      End If
    Next i%
    
End Sub

Sub chkChangePrem_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkChangePrem_Click

End Sub

Sub chkLoan_Click ()

    lblLoan(0).Visible = chkLoan.Value
    lblLoan(1).Visible = chkLoan.Value
    lblLoan(2).Visible = chkLoan.Value
    lblLoan(3).Visible = chkLoan.Value
    lblLoan(4).Visible = chkLoan.Value
    lblLoan(5).Visible = chkLoan.Value
    For i% = 0 To 4
      txtLoanStart(i%).Visible = chkLoan.Value
      txtLoan(i%).Visible = chkLoan.Value
      txtLoanEnd(i%).Visible = chkLoan.Value
      If chkLoan.Value = 0 Then
         txtLoanStart(i%).Text = "__"
         txtLoan(i%).Text = ""
         txtLoanEnd(i%).Text = "__"
      End If
    Next i%
    
End Sub

Sub chkLoan_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkLoan_Click

End Sub

Sub chkLoanRepay_Click ()

    lblLoanRepay(0).Visible = chkLoanRepay.Value
    lblLoanRepay(1).Visible = chkLoanRepay.Value
    lblLoanRepay(2).Visible = chkLoanRepay.Value
    lblLoanRepay(3).Visible = chkLoanRepay.Value
    lblLoanRepay(4).Visible = chkLoanRepay.Value
    lblLoanRepay(5).Visible = chkLoanRepay.Value
    For i% = 0 To 4
      txtLoanRepayStart(i%).Visible = chkLoanRepay.Value
      txtLoanRepay(i%).Visible = chkLoanRepay.Value
      txtLoanRepayEnd(i%).Visible = chkLoanRepay.Value
      If chkLoanRepay.Value = 0 Then
         txtLoanRepayStart(i%).Text = "__"
         txtLoanRepay(i%).Text = ""
         txtLoanRepayEnd(i%).Text = "__"
      End If
    Next i%
    
End Sub

Sub chkLoanRepay_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkLoanRepay_Click

End Sub

Sub chkWithdraw_Click ()

    lblWithdraw(0).Visible = chkWithdraw.Value
    lblWithdraw(1).Visible = chkWithdraw.Value
    lblWithdraw(2).Visible = chkWithdraw.Value
    lblWithdraw(3).Visible = chkWithdraw.Value
    lblWithdraw(4).Visible = chkWithdraw.Value
    lblWithdraw(5).Visible = chkWithdraw.Value
    For i% = 0 To 4
      txtWithDrawStart(i%).Visible = chkWithdraw.Value
      txtWithdraw(i%).Visible = chkWithdraw.Value
      txtWithdrawEnd(i%).Visible = chkWithdraw.Value
      If chkWithdraw.Value = 0 Then
         txtWithDrawStart(i%).Text = "__"
         txtWithdraw(i%).Text = ""
         txtWithdrawEnd(i%).Text = "__"
      End If
    Next i%

End Sub

Sub chkWithdraw_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkWithdraw_Click

End Sub

Sub cmdExit_Click ()
    
    Unload frmLSCHANGE
    If SpouseClient = 1 Then
       frmLSSPOUSE.Show 0
    Else
       frmLIFESV.Show 0
    End If

End Sub

Sub cmdPrem_Click ()
    
    Unload frmLSCHANGE
    frmLSPREM.Show 0

End Sub

Sub cmdRiders_Click ()

    Unload frmLSCHANGE
    frmLSINSD.Show 0

End Sub

Sub Form_Load ()

    Call Load_Future_Changes

    If SpouseClient = 1 Then
       cmdExit.Caption = "E&XIT (to SPOUSE)"
       cmdRiders.Enabled = False
    Else
       cmdExit.Caption = "E&XIT (to CLIENT)"
       cmdRiders.Enabled = True
    End If
    lblChangeFace(0).Visible = chkChangeFace.Value
    lblChangePrem(0).Visible = chkChangePrem.Value
    lblChangeCurrInt(0).Visible = chkChangeCurrInt.Value
    lblChangeDBOpt(0).Visible = chkChangeDBOpt.Value
    lblChangeFace(1).Visible = chkChangeFace.Value
    lblChangePrem(1).Visible = chkChangePrem.Value
    lblChangeCurrInt(1).Visible = chkChangeCurrInt.Value
    lblChangeDBOpt(1).Visible = chkChangeDBOpt.Value
    lblWithdraw(0).Visible = chkWithdraw.Value
    lblWithdraw(1).Visible = chkWithdraw.Value
    lblWithdraw(2).Visible = chkWithdraw.Value
    lblWithdraw(3).Visible = chkWithdraw.Value
    lblWithdraw(4).Visible = chkWithdraw.Value
    lblWithdraw(5).Visible = chkWithdraw.Value
    lblLoan(0).Visible = chkLoan.Value
    lblLoan(1).Visible = chkLoan.Value
    lblLoan(2).Visible = chkLoan.Value
    lblLoan(3).Visible = chkLoan.Value
    lblLoan(4).Visible = chkLoan.Value
    lblLoan(5).Visible = chkLoan.Value
    lblLoanRepay(0).Visible = chkLoanRepay.Value
    lblLoanRepay(1).Visible = chkLoanRepay.Value
    lblLoanRepay(2).Visible = chkLoanRepay.Value
    lblLoanRepay(3).Visible = chkLoanRepay.Value
    lblLoanRepay(4).Visible = chkLoanRepay.Value
    lblLoanRepay(5).Visible = chkLoanRepay.Value
    For i% = 0 To 4
      txtChangeFaceAge(i%).Visible = chkChangeFace.Value
      txtChangeFace(i%).Visible = chkChangeFace.Value
      txtChangePremAge(i%).Visible = chkChangePrem.Value
      txtChangePrem(i%).Visible = chkChangePrem.Value
      txtChangeCurrIntAge(i%).Visible = chkChangeCurrInt.Value
      txtChangeCurrInt(i%).Visible = chkChangeCurrInt.Value
      txtChangeDBOptAge(i%).Visible = chkChangeDBOpt.Value
      txtChangeDBOpt(i%).Visible = chkChangeDBOpt.Value
      txtWithDrawStart(i%).Visible = chkWithdraw.Value
      txtWithdraw(i%).Visible = chkWithdraw.Value
      txtWithdrawEnd(i%).Visible = chkWithdraw.Value
      txtLoanStart(i%).Visible = chkLoan.Value
      txtLoan(i%).Visible = chkLoan.Value
      txtLoanEnd(i%).Visible = chkLoan.Value
      txtLoanRepayStart(i%).Visible = chkLoanRepay.Value
      txtLoanRepay(i%).Visible = chkLoanRepay.Value
      txtLoanRepayEnd(i%).Visible = chkLoanRepay.Value
    Next i%

End Sub

Sub Form_Unload (Cancel As Integer)

    Call Set_Future_Changes
    Set frmLSCHANGE = Nothing

End Sub

Sub txtChangeCurrInt_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeCurrInt(Index).Text)
    txtChangeCurrInt(Index).Text = ""
    txtChangeCurrInt(Index).Text = clipboard.GetText()
    txtChangeCurrInt(Index).ForeColor = &H80000005
    txtChangeCurrInt(Index).BackColor = &H80000008

End Sub

Sub txtChangeCurrInt_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeCurrInt(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangeCurrInt(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeCurrInt(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeCurrIntAge(Index + 1).SetFocus
       End If
    End If
    
End Sub

Sub txtChangeCurrInt_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtChangeCurrInt_LostFocus (Index As Integer)
    
    Dim TempInt As Single

    txtChangeCurrInt(Index).ForeColor = &H80000008
    txtChangeCurrInt(Index).BackColor = &H80000005
    If Val(txtChangeCurrInt(Index).Text) = 0 Then
       txtChangeCurrIntAge(Index).Text = "__"
    End If
    If Val(txtChangeCurrInt(Index).Text) <> 0 And (Val(txtChangeCurrInt(Index).Text) < 4# Or Val(txtChangeCurrInt(Index).Text) > 20#) Then
       msgline$ = "Change Interest Amount must be between 4.00% and 20.00%, inclusive"
       MsgBox msgline$
       txtChangeCurrInt(Index).Text = ""
       txtChangeCurrInt(Index).SetFocus
       Exit Sub
    End If
    TempInt = Val(txtChangeCurrInt(Index).Text)
    If TempInt > 0 Then txtChangeCurrInt(Index).Text = Format$(TempInt, "00.000")
    
End Sub

Sub txtChangeCurrIntAge_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeCurrIntAge(Index).Text)
    txtChangeCurrIntAge(Index).Text = "__"
    txtChangeCurrIntAge(Index).Text = clipboard.GetText()
    txtChangeCurrIntAge(Index).ForeColor = &H80000005
    txtChangeCurrIntAge(Index).BackColor = &H80000008

End Sub

Sub txtChangeCurrIntAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtChangeCurrIntAge(Index).Text = "__" Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeCurrIntAge(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangeCurrIntAge(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeCurrIntAge(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtChangeCurrIntAge(Index).Text = "__" Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeCurrInt(Index).SetFocus
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChangeCurrIntAge(Index).Text = clipboard.GetText() Then
          txtChangeCurrIntAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtChangeCurrIntAge(Index).Text) > 9 Then
          Call txtChangeCurrIntAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangeCurrIntAge(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for change must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for change must be less than age 95"
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangeCurrIntAge(Index - 1).Text) Or txtChangeCurrIntAge(Index - 1).Text = "" Then
             msgtext$ = "Age for change must be greater than previous 'change age'"
          End If
       End If
    Else
       txtChangeCurrInt(Index).Text = ""
       txtChangeCurrIntAge(Index).Text = "__"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtChangeCurrIntAge(Index).Text = "__"
       txtChangeCurrIntAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangeCurrIntAge(Index).ForeColor = &H80000008
    txtChangeCurrIntAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangeCurrInt(Index).SetFocus

End Sub

Sub txtChangeDBOpt_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeDBOpt(Index).Text)
    txtChangeDBOpt(Index).Text = "_"
    txtChangeDBOpt(Index).Text = clipboard.GetText()
    txtChangeDBOpt(Index).ForeColor = &H80000005
    txtChangeDBOpt(Index).BackColor = &H80000008

End Sub

Sub txtChangeDBOpt_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkWithdraw.SetFocus
       Else
          txtChangeDBOpt(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeDBOpt(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeDBOpt(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkWithdraw.SetFocus
       Else
          txtChangeDBOptAge(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtChangeDBOpt_LostFocus (Index As Integer)
    
    txtChangeDBOpt(Index).ForeColor = &H80000008
    txtChangeDBOpt(Index).BackColor = &H80000005
    If Val(txtChangeDBOpt(Index).Text) < 1 Or Val(txtChangeDBOpt(Index).Text) > 2 Then
       msgline$ = "Change DB Option must be either 1 (level) or 2 (increasing)"
       MsgBox msgline$
       txtChangeDBOpt(Index).Text = "_"
       txtChangeDBOpt(Index).SetFocus
       Exit Sub
    End If
    If (Index > 0) Or (Index = 0 And Val(txtChangeDBOpt(0).Text) = LSClient(SpouseClient).Option) Then
       If Index > 0 Then
          If txtChangeDBOpt(Index).Text <> txtChangeDBOpt(Index - 1).Text Then
             Exit Sub
          End If
       End If
       msgline$ = "New DB Option must be different from previous DB Option"
       MsgBox msgline$
       txtChangeDBOpt(Index).Text = "_"
       txtChangeDBOpt(Index).SetFocus
       Exit Sub
    End If

End Sub

Sub txtChangeDBOptAge_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeDBOptAge(Index).Text)
    txtChangeDBOptAge(Index).Text = "__"
    txtChangeDBOptAge(Index).Text = clipboard.GetText()
    txtChangeDBOptAge(Index).ForeColor = &H80000005
    txtChangeDBOptAge(Index).BackColor = &H80000008

End Sub

Sub txtChangeDBOptAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtChangeDBOptAge(Index).Text = "__" Then
          chkWithdraw.SetFocus
       Else
          txtChangeDBOptAge(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeDBOpt.SetFocus
       Else
          txtChangeDBOptAge(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeDBOptAge(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtChangeDBOptAge(Index).Text = "__" Then
          chkWithdraw.SetFocus
       Else
          txtChangeDBOpt(Index).SetFocus
       End If
    End If

End Sub

Sub txtChangeDBOptAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChangeDBOptAge(Index).Text = clipboard.GetText() Then
          txtChangeDBOptAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangeDBOptAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtChangeDBOptAge(Index).Text) > 9 Then
          Call txtChangeDBOptAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangeDBOptAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangeDBOptAge(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for change must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for change must be less than age 95"
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangeDBOptAge(Index - 1).Text) Or txtChangeDBOptAge(Index - 1).Text = "" Then
             msgtext$ = "Age for change must be greater than previous 'change age'"
          End If
       End If
    Else
       txtChangeDBOpt(Index).Text = "_"
       txtChangeDBOptAge(Index).Text = "__"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtChangeDBOptAge(Index).Text = "__"
       txtChangeDBOptAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangeDBOptAge(Index).ForeColor = &H80000008
    txtChangeDBOptAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangeDBOpt(Index).SetFocus

End Sub

Sub txtChangeFace_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeFace(Index).Text)
    txtChangeFace(Index).Text = ""
    txtChangeFace(Index).Text = clipboard.GetText()
    txtChangeFace(Index).ForeColor = &H80000005
    txtChangeFace(Index).BackColor = &H80000008

End Sub

Sub txtChangeFace_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkChangePrem.SetFocus
       Else
          txtChangeFace(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeFace.SetFocus
       Else
          txtChangeFace(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeFace(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkChangePrem.SetFocus
       Else
          txtChangeFaceAge(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtChangeFace_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtChangeFace_LostFocus (Index As Integer)
    
    Dim TempValue As Single

    txtChangeFace(Index).ForeColor = &H80000008
    txtChangeFace(Index).BackColor = &H80000005
    If Val(txtChangeFace(Index).Text) = 0 Then
       txtChangeFaceAge(Index).Text = "__"
    End If
    If Val(txtChangeFace(Index).Text) <> 0 And (Val(txtChangeFace(Index).Text) < 15000 Or Val(txtChangeFace(Index).Text) > 9999999) Then
       msgline$ = "Change Death Benefit Amount must be between 15000 and 9999999, inclusive"
       MsgBox msgline$
       txtChangeFace(Index).Text = ""
       txtChangeFace(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtChangeFace(Index).Text)
    If TempValue > 0 Then txtChangeFace(Index).Text = Format$(TempValue, "###00000")

End Sub

Sub txtChangeFaceAge_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtChangeFaceAge(Index).Text)
    txtChangeFaceAge(Index).Text = "__"
    txtChangeFaceAge(Index).Text = clipboard.GetText()
    txtChangeFaceAge(Index).ForeColor = &H80000005
    txtChangeFaceAge(Index).BackColor = &H80000008

End Sub

Sub txtChangeFaceAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtChangeFaceAge(Index).Text = "__" Then
          chkChangePrem.SetFocus
       Else
          txtChangeFaceAge(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangeFace.SetFocus
       Else
          txtChangeFaceAge(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangeFaceAge(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtChangeFaceAge(Index).Text = "__" Then
          chkChangePrem.SetFocus
       Else
          txtChangeFace(Index).SetFocus
       End If
    End If

End Sub

Sub txtChangeFaceAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChangeFaceAge(Index).Text = clipboard.GetText() Then
          txtChangeFaceAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangeFaceAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtChangeFaceAge(Index).Text) > 9 Then
          Call txtChangeFaceAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangeFaceAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangeFaceAge(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for change must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for change must be less than age 95"
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangeFaceAge(Index - 1).Text) Or txtChangeFaceAge(Index - 1).Text = "" Then
             msgtext$ = "Age for change must be greater than previous 'change age'"
          End If
       End If
    Else
       txtChangeFace(Index).Text = ""
       txtChangeFaceAge(Index).Text = "__"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtChangeFaceAge(Index).Text = "__"
       txtChangeFaceAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangeFaceAge(Index).ForeColor = &H80000008
    txtChangeFaceAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangeFace(Index).SetFocus

End Sub

Sub txtChangePrem_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtChangePrem(Index).Text)
    txtChangePrem(Index).Text = ""
    txtChangePrem(Index).Text = clipboard.GetText()
    txtChangePrem(Index).ForeColor = &H80000005
    txtChangePrem(Index).BackColor = &H80000008

End Sub

Sub txtChangePrem_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangePrem(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangePrem.SetFocus
       Else
          txtChangePrem(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangePrem(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangePremAge(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtChangePrem_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtChangePrem_LostFocus (Index As Integer)
    
    Dim TempPrem As Single

    txtChangePrem(Index).ForeColor = &H80000008
    txtChangePrem(Index).BackColor = &H80000005
    If Val(txtChangePrem(Index).Text) > 999999.99 Then
       msgline$ = "Change Premium Amount must be less than 999999.99"
       MsgBox msgline$
       txtChangePrem(Index).Text = ""
       txtChangePrem(Index).SetFocus
       Exit Sub
    End If
    TempPrem = Val(txtChangePrem(Index).Text)
    If TempPrem > 0 Then txtChangePrem(Index).Text = Format$(TempPrem, "###000.00")

End Sub

Sub txtChangePremAge_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtChangePremAge(Index).Text)
    txtChangePremAge(Index).Text = "__"
    txtChangePremAge(Index).Text = clipboard.GetText()
    txtChangePremAge(Index).ForeColor = &H80000005
    txtChangePremAge(Index).BackColor = &H80000008

End Sub

Sub txtChangePremAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtChangePremAge(Index).Text = "__" Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangePremAge(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkChangePrem.SetFocus
       Else
          txtChangePremAge(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtChangePremAge(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtChangePremAge(Index).Text = "__" Then
          chkChangeCurrInt.SetFocus
       Else
          txtChangePrem(Index).SetFocus
       End If
    End If

End Sub

Sub txtChangePremAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChangePremAge(Index).Text = clipboard.GetText() Then
          txtChangePremAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangePremAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtChangePremAge(Index).Text) > 9 Then
          Call txtChangePremAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangePremAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangePremAge(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for change must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for change must be less than age 95"
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangePremAge(Index - 1).Text) Or txtChangePremAge(Index - 1).Text = "" Then
             msgtext$ = "Age for change must be greater than previous 'change age'"
          End If
       End If
    Else
       txtChangePrem(Index).Text = ""
       txtChangePremAge(Index).Text = "__"
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtChangePremAge(Index).Text = "__"
       txtChangePremAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangePremAge(Index).ForeColor = &H80000008
    txtChangePremAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangePrem(Index).SetFocus

End Sub

Sub txtLoan_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtLoan(Index).Text)
    txtLoan(Index).Text = ""
    txtLoan(Index).Text = clipboard.GetText()
    txtLoan(Index).ForeColor = &H80000005
    txtLoan(Index).BackColor = &H80000008

End Sub

Sub txtLoan_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkLoanRepay.SetFocus
       Else
          txtLoan(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoan.SetFocus
       Else
          txtLoan(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoan(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       txtLoanEnd(Index).SetFocus
    End If

End Sub

Sub txtLoan_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtLoan_LostFocus (Index As Integer)
    
    Dim TempValue As Single

    txtLoan(Index).ForeColor = &H80000008
    txtLoan(Index).BackColor = &H80000005
    If Val(txtLoan(Index).Text) = 0 Then
       txtLoanStart(Index).Text = "__"
       txtLoanEnd(Index).Text = "__"
    End If
    If Val(txtLoan(Index).Text) <> 0 And (Val(txtLoan(Index).Text) < 100 Or Val(txtLoan(Index).Text) > 999999) Then
       msgline$ = "Loan Amount must be between 100 and 999999, inclusive"
       MsgBox msgline$
       txtLoan(Index).Text = ""
       txtLoan(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtLoan(Index).Text)
    If TempValue > 0 Then txtLoan(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtLoanEnd_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtLoanEnd(Index).Text)
    txtLoanEnd(Index).Text = "__"
    txtLoanEnd(Index).Text = clipboard.GetText()
    txtLoanEnd(Index).ForeColor = &H80000005
    txtLoanEnd(Index).BackColor = &H80000008

End Sub

Sub txtLoanEnd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtLoanEnd(Index).Text = "__" Then
          chkLoanRepay.SetFocus
       Else
          txtLoanEnd(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoan.SetFocus
       Else
          txtLoanEnd(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoanEnd(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkLoanRepay.SetFocus
       Else
          txtLoanStart(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtLoanEnd_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtLoanEnd(Index).Text = clipboard.GetText() Then
          txtLoanEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtLoanEnd(Index).Text) > 9 Then
          Call txtLoanEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanEnd_LostFocus (Index As Integer)

    chgage% = Val(txtLoanEnd(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for loan must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for loan must be less than age 95"
       End If
       If chgage% < Val(txtLoanStart(Index).Text) Or txtLoanStart(Index).Text = "" Then
          msgtext$ = "Age for loan end must be greater than or equal to loan start age"
       End If
    Else
       If Val(txtLoanStart(Index).Text) > 0 Then
          msgtext$ = "Age for end of loan MUST be entered"
       Else
          msgtext$ = ""
       End If
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtLoanEnd(Index).Text = "__"
       txtLoanEnd(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanEnd(Index).ForeColor = &H80000008
    txtLoanEnd(Index).BackColor = &H80000005
    If chgage% > 0 Then
       If Index > 3 Then
          chkLoanRepay.SetFocus
       Else
          txtLoanStart(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtLoanRepay_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtLoanRepay(Index).Text)
    txtLoanRepay(Index).Text = "__"
    txtLoanRepay(Index).Text = clipboard.GetText()
    txtLoanRepay(Index).ForeColor = &H80000005
    txtLoanRepay(Index).BackColor = &H80000008

End Sub

Sub txtLoanRepay_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          cmdExit.SetFocus
       Else
          txtLoanRepay(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoanRepay.SetFocus
       Else
          txtLoanRepay(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoanRepay(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       txtLoanRepayEnd(Index).SetFocus
    End If

End Sub

Sub txtLoanRepay_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtLoanRepay_LostFocus (Index As Integer)
    
    Dim TempValue As Single

    txtLoanRepay(Index).ForeColor = &H80000008
    txtLoanRepay(Index).BackColor = &H80000005
    If Val(txtLoanRepay(Index).Text) = 0 Then
       txtLoanRepayStart(Index).Text = "__"
       txtLoanRepayEnd(Index).Text = "__"
    End If
    If Val(txtLoanRepay(Index).Text) <> 0 And (Val(txtLoanRepay(Index).Text) < 10 Or Val(txtLoanRepay(Index).Text) > 999999) Then
       msgline$ = "Loan Repay Amount must be between 10 and 999999, inclusive"
       MsgBox msgline$
       txtLoanRepay(Index).Text = "__"
       txtLoanRepay(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtLoanRepay(Index).Text)
    If TempValue > 0 Then txtLoanRepay(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtLoanRepayEnd_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtLoanRepayEnd(Index).Text)
    txtLoanRepayEnd(Index).Text = "__"
    txtLoanRepayEnd(Index).Text = clipboard.GetText()
    txtLoanRepayEnd(Index).ForeColor = &H80000005
    txtLoanRepayEnd(Index).BackColor = &H80000008

End Sub

Sub txtLoanRepayEnd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtLoanRepayEnd(Index).Text = "__" Then
          cmdExit.SetFocus
       Else
          txtLoanRepayEnd(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoanRepay.SetFocus
       Else
          txtLoanRepayEnd(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoanRepayEnd(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtLoanRepayEnd(Index).Text = "__" Then
          cmdExit.SetFocus
       Else
          txtLoanRepay(Index).SetFocus
       End If
    End If

End Sub

Sub txtLoanRepayEnd_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtLoanRepayEnd(Index).Text = clipboard.GetText() Then
          txtLoanRepayEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanRepayEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtLoanRepayEnd(Index).Text) > 9 Then
          Call txtLoanRepayEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanRepayEnd_LostFocus (Index As Integer)

    chgage% = Val(txtLoanRepayEnd(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for Loan Repay must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for Loan Repay must be less than age 95"
       End If
       If chgage% < Val(txtLoanRepayStart(Index).Text) Or txtLoanRepayStart(Index).Text = "" Then
          msgtext$ = "Age for Loan Repay end must be greater than or equal to Loan Repay start age"
       End If
    Else
       If Val(txtLoanRepayStart(Index).Text) > 0 Then
          msgtext$ = "Age for end of loan repayment MUST be entered"
       Else
          msgtext$ = ""
       End If
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtLoanRepayEnd(Index).Text = "__"
       txtLoanRepayEnd(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanRepayEnd(Index).ForeColor = &H80000008
    txtLoanRepayEnd(Index).BackColor = &H80000005
    If chgage% > 0 Then
       If Index > 3 Then
          cmdExit.SetFocus
       Else
          txtLoanRepayStart(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtLoanRepayStart_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtLoanRepayStart(Index).Text)
    txtLoanRepayStart(Index).Text = "__"
    txtLoanRepayStart(Index).Text = clipboard.GetText()
    txtLoanRepayStart(Index).ForeColor = &H80000005
    txtLoanRepayStart(Index).BackColor = &H80000008

End Sub

Sub txtLoanRepayStart_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtLoanRepayStart(Index).Text = "__" Then
          cmdExit.SetFocus
       Else
          txtLoanRepayStart(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoanRepay.SetFocus
       Else
          txtLoanRepayStart(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoanRepayStart(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtLoanRepayStart(Index).Text = "__" Then
          cmdExit.SetFocus
       Else
          txtLoanRepay(Index).SetFocus
       End If
    End If

End Sub

Sub txtLoanRepayStart_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtLoanRepayStart(Index).Text = clipboard.GetText() Then
          txtLoanRepayStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanRepayStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtLoanRepayStart(Index).Text) > 9 Then
          Call txtLoanRepayStart_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanRepayStart_LostFocus (Index As Integer)

    chgage% = Val(txtLoanRepayStart(Index))
    loanage% = 0
    For i% = 4 To 0 Step -1
      If Val(txtLoanStart(i%)) > 0 Then loanage% = Val(txtLoanStart(i%))
    Next i%
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for Loan Repay must be greater than Client issue age"
       End If
       If chgage% < loanage% Then
          msgtext$ = "A loan at an earlier age must exist before creating a Loan Repay"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for Loan Repay must be less than age 95"
       End If
       If Index > 0 Then
          tempage% = Val(txtLoanRepayEnd(Index - 1).Text)
          If chgage% <= Val(txtLoanRepayStart(Index - 1).Text) Or txtLoanRepayStart(Index - 1).Text = "" Or chgage% <= tempage% Then
             msgtext$ = "Age for Loan Repay must be greater than age for previous 'Loan Repay period'"
          End If
       End If
    Else
       txtLoanRepayStart(Index).Text = "__"
       txtLoanRepayEnd(Index).Text = "__"
       txtLoanRepay(Index).Text = ""
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtLoanRepayStart(Index).Text = "__"
       txtLoanRepayStart(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanRepayStart(Index).ForeColor = &H80000008
    txtLoanRepayStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtLoanRepay(Index).SetFocus

End Sub

Sub txtLoanStart_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtLoanStart(Index).Text)
    txtLoanStart(Index).Text = "__"
    txtLoanStart(Index).Text = clipboard.GetText()
    txtLoanStart(Index).ForeColor = &H80000005
    txtLoanStart(Index).BackColor = &H80000008

End Sub

Sub txtLoanStart_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtLoanStart(Index).Text = "__" Then
          chkLoanRepay.SetFocus
       Else
          txtLoanStart(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkLoan.SetFocus
       Else
          txtLoanStart(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtLoanStart(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtLoanStart(Index).Text = "__" Then
          chkLoanRepay.SetFocus
       Else
          txtLoan(Index).SetFocus
       End If
    End If

End Sub

Sub txtLoanStart_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtLoanStart(Index).Text = clipboard.GetText() Then
          txtLoanStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtLoanStart(Index).Text) > 9 Then
          Call txtLoanStart_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanStart_LostFocus (Index As Integer)

    chgage% = Val(txtLoanStart(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for Loan must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for Loan must be less than age 95"
       End If
       If Index > 0 Then
          tempage% = Val(txtLoanEnd(Index - 1).Text)
          If chgage% <= Val(txtLoanStart(Index - 1).Text) Or txtLoanStart(Index - 1).Text = "" Or chgage% <= tempage% Then
             msgtext$ = "Age for loan must be greater than age for previous 'loan period'"
          End If
       End If
    Else
       txtLoanStart(Index).Text = "__"
       txtLoanEnd(Index).Text = "__"
       txtLoan(Index).Text = ""
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtLoanStart(Index).Text = "__"
       txtLoanStart(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanStart(Index).ForeColor = &H80000008
    txtLoanStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtLoan(Index).SetFocus

End Sub

Sub txtWithdraw_GotFocus (Index As Integer)
    
    clipboard.Clear
    clipboard.SetText Trim$(txtWithdraw(Index).Text)
    txtWithdraw(Index).Text = ""
    txtWithdraw(Index).Text = clipboard.GetText()
    txtWithdraw(Index).ForeColor = &H80000005
    txtWithdraw(Index).BackColor = &H80000008

End Sub

Sub txtWithdraw_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)
    
    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkLoan.SetFocus
       Else
          txtWithdraw(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkWithdraw.SetFocus
       Else
          txtWithdraw(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithdraw(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       txtWithdrawEnd(Index).SetFocus
    End If

End Sub

Sub txtWithdraw_KeyPress (Index As Integer, KeyAscii As Integer)

    If (KeyAscii < 48 Or KeyAscii > 57) And KeyAscii <> 46 Then KeyAscii = 0

End Sub

Sub txtWithdraw_LostFocus (Index As Integer)
    
    Dim TempValue As Single

    txtWithdraw(Index).ForeColor = &H80000008
    txtWithdraw(Index).BackColor = &H80000005
    If Val(txtWithdraw(Index).Text) = 0 Then
       txtWithDrawStart(Index).Text = "__"
       txtWithdrawEnd(Index).Text = "__"
    End If
    If Val(txtWithdraw(Index).Text) <> 0 And (Val(txtWithdraw(Index).Text) < 100 Or Val(txtWithdraw(Index).Text) > 999999) Then
       msgline$ = "Withdraw Amount must be between 100 and 999999, inclusive"
       MsgBox msgline$
       txtWithdraw(Index).Text = ""
       txtWithdraw(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtWithdraw(Index).Text)
    If TempValue > 0 Then txtWithdraw(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtWithdrawEnd_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtWithdrawEnd(Index).Text)
    txtWithdrawEnd(Index).Text = "__"
    txtWithdrawEnd(Index).Text = clipboard.GetText()
    txtWithdrawEnd(Index).ForeColor = &H80000005
    txtWithdrawEnd(Index).BackColor = &H80000008

End Sub

Sub txtWithdrawEnd_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtWithdrawEnd(Index).Text = "__" Then
          chkLoan.SetFocus
       Else
          txtWithdrawEnd(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkWithdraw.SetFocus
       Else
          txtWithdrawEnd(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithdrawEnd(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtWithdrawEnd(Index).Text = "__" Then
          chkLoan.SetFocus
       Else
          txtWithdraw(Index).SetFocus
       End If
    End If

End Sub

Sub txtWithdrawEnd_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtWithdrawEnd(Index).Text = clipboard.GetText() Then
          txtWithdrawEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtWithdrawEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtWithdrawEnd(Index).Text) > 9 Then
          Call txtWithdrawEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtWithdrawEnd_LostFocus (Index As Integer)

    chgage% = Val(txtWithdrawEnd(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for withdraw must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for withdraw must be less than age 95"
       End If
       If chgage% < Val(txtWithDrawStart(Index).Text) Or txtWithDrawStart(Index).Text = "" Then
          msgtext$ = "Age for withdraw end must be greater than or equal to withdraw start"
       End If
    Else
       If Val(txtWithDrawStart(Index).Text) > 0 Then
          msgtext$ = "Age for end of withdrawals MUST be entered"
       Else
          msgtext$ = ""
       End If
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtWithdrawEnd(Index).Text = "__"
       txtWithdrawEnd(Index).SetFocus
       Exit Sub
    End If
    
    txtWithdrawEnd(Index).ForeColor = &H80000008
    txtWithdrawEnd(Index).BackColor = &H80000005
    If chgage% > 0 Then
       If Index > 3 Then
          chkLoan.SetFocus
       Else
          txtWithDrawStart(Index + 1).SetFocus
       End If
    End If

End Sub

Sub txtWithdrawStart_GotFocus (Index As Integer)
 
    clipboard.Clear
    clipboard.SetText Trim$(txtWithDrawStart(Index).Text)
    txtWithDrawStart(Index).Text = "__"
    txtWithDrawStart(Index).Text = clipboard.GetText()
    txtWithDrawStart(Index).ForeColor = &H80000005
    txtWithDrawStart(Index).BackColor = &H80000008

End Sub

Sub txtWithdrawStart_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtWithDrawStart(Index).Text = "__" Then
          chkLoan.SetFocus
       Else
          txtWithDrawStart(Index + 1).SetFocus
       End If
    End If
    If KeyCode = KEY_UP Then
       If Index = 0 Then
          chkWithdraw.SetFocus
       Else
          txtWithDrawStart(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithDrawStart(Index).Text = clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtWithDrawStart(Index).Text = "__" Then
          chkLoan.SetFocus
       Else
          txtWithdraw(Index).SetFocus
       End If
    End If

End Sub

Sub txtWithdrawStart_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtWithDrawStart(Index).Text = clipboard.GetText() Then
          txtWithDrawStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtWithdrawStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode > 47 And KeyCode < 58 Then
       If Val(txtWithDrawStart(Index).Text) > 9 Then
          Call txtWithdrawStart_LostFocus(Index)
       End If
    End If

End Sub

Sub txtWithdrawStart_LostFocus (Index As Integer)

    chgage% = Val(txtWithDrawStart(Index))
    msgtext$ = ""
    If chgage% <> 0 Then
       tempage% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < tempage% Then
          msgtext$ = "Age for withdraw must be greater than Client issue age"
       End If
       If chgage% > 95 Then
          msgtext$ = "Age for withdraw must be less than age 95"
       End If
       If Index > 0 Then
          tempage% = Val(txtWithdrawEnd(Index - 1).Text)
          If chgage% <= Val(txtWithDrawStart(Index - 1).Text) Or txtWithDrawStart(Index - 1).Text = "" Or chgage% <= tempage% Then
             msgtext$ = "Age for withdraw must be greater than age for previous 'withdraw period'"
          End If
       End If
    Else
       txtWithDrawStart(Index).Text = "__"
       txtWithdrawEnd(Index).Text = "__"
       txtWithdraw(Index).Text = ""
    End If
    If msgtext$ <> "" Then
       MsgBox msgtext$
       txtWithDrawStart(Index).Text = "__"
       txtWithDrawStart(Index).SetFocus
       Exit Sub
    End If
    
    txtWithDrawStart(Index).ForeColor = &H80000008
    txtWithDrawStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtWithdraw(Index).SetFocus

End Sub

