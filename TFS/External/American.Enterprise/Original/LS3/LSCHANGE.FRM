VERSION 2.00
Begin Form frmLSCHANGE 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "LifeSavings - Changes in Future Illustration Years"
   ClientHeight    =   6915
   ClientLeft      =   60
   ClientTop       =   420
   ClientWidth     =   9585
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
   Begin Frame fraWDandLoan 
      Caption         =   "Future Withdrawals, Loans and Loan Repayments"
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   2895
      Left            =   240
      TabIndex        =   90
      Top             =   3000
      Width           =   9135
      Begin CheckBox chkLoanRepay 
         Caption         =   "  Annual Loan Repayments"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   312
         Left            =   6240
         TabIndex        =   65
         Top             =   240
         Width           =   2652
      End
      Begin CheckBox chkLoan 
         Caption         =   "  Annual Policy Loan"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   3240
         TabIndex        =   49
         Top             =   240
         Width           =   2655
      End
      Begin Frame fraLoanRepay 
         Height          =   2295
         Left            =   6240
         TabIndex        =   85
         Top             =   480
         Width           =   2655
         Begin TextBox txtLoanRepay 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   79
            Top             =   1920
            Width           =   1215
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   76
            Top             =   1560
            Width           =   1215
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   73
            Top             =   1200
            Width           =   1215
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   70
            Top             =   840
            Width           =   1215
         End
         Begin TextBox txtLoanRepay 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   67
            Top             =   480
            Width           =   1215
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   80
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   77
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   74
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   71
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   78
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   75
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   72
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   69
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   68
            Top             =   480
            Width           =   375
         End
         Begin MaskEdBox txtLoanRepayStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   66
            Top             =   480
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   5
            Left            =   2160
            TabIndex        =   115
            Top             =   285
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   4
            Left            =   720
            TabIndex        =   114
            Top             =   285
            Width           =   1215
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   3
            Left            =   120
            TabIndex        =   113
            Top             =   285
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   2
            Left            =   2160
            TabIndex        =   102
            Top             =   100
            Width           =   375
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Annual Repay"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   1
            Left            =   720
            TabIndex        =   103
            Top             =   100
            Width           =   1215
         End
         Begin Label lblLoanRepay 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   0
            Left            =   120
            TabIndex        =   86
            Top             =   100
            Width           =   375
         End
      End
      Begin Frame fraLoans 
         Height          =   2295
         Left            =   3240
         TabIndex        =   87
         Top             =   480
         Width           =   2655
         Begin TextBox txtLoan 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   63
            Top             =   1920
            Width           =   1215
         End
         Begin TextBox txtLoan 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   60
            Top             =   1560
            Width           =   1215
         End
         Begin TextBox txtLoan 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   57
            Top             =   1200
            Width           =   1215
         End
         Begin TextBox txtLoan 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   54
            Top             =   840
            Width           =   1215
         End
         Begin TextBox txtLoan 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   51
            Top             =   480
            Width           =   1215
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   64
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   61
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   58
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   55
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   62
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   59
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   56
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   53
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtLoanStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   50
            Top             =   480
            Width           =   375
         End
         Begin MaskEdBox txtLoanEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   52
            Top             =   480
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   5
            Left            =   2160
            TabIndex        =   112
            Top             =   285
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   4
            Left            =   720
            TabIndex        =   111
            Top             =   285
            Width           =   1215
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   3
            Left            =   120
            TabIndex        =   110
            Top             =   285
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   2
            Left            =   2160
            TabIndex        =   101
            Top             =   100
            Width           =   375
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Annual Loan"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   1
            Left            =   720
            TabIndex        =   100
            Top             =   100
            Width           =   1215
         End
         Begin Label lblLoan 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   0
            Left            =   120
            TabIndex        =   88
            Top             =   100
            Width           =   375
         End
      End
      Begin CheckBox chkWithdraw 
         Caption         =   "  Withdrawals"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   240
         TabIndex        =   33
         Top             =   240
         Width           =   2415
      End
      Begin Frame fraWithdraw 
         Height          =   2295
         Left            =   240
         TabIndex        =   89
         Top             =   480
         Width           =   2655
         Begin TextBox txtWithdraw 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   720
            MaxLength       =   9
            TabIndex        =   47
            Top             =   1920
            Width           =   1215
         End
         Begin TextBox txtWithdraw 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   720
            MaxLength       =   9
            TabIndex        =   44
            Top             =   1560
            Width           =   1215
         End
         Begin TextBox txtWithdraw 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   720
            MaxLength       =   9
            TabIndex        =   41
            Top             =   1200
            Width           =   1215
         End
         Begin TextBox txtWithdraw 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   720
            MaxLength       =   9
            TabIndex        =   38
            Top             =   840
            Width           =   1215
         End
         Begin TextBox txtWithdraw 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   720
            MaxLength       =   9
            TabIndex        =   35
            Top             =   480
            Width           =   1215
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   48
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   45
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   42
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   39
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawEnd 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   2160
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   36
            Top             =   480
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   46
            Top             =   1920
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   43
            Top             =   1560
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   40
            Top             =   1200
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   37
            Top             =   840
            Width           =   375
         End
         Begin MaskEdBox txtWithdrawStart 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   120
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   34
            Top             =   480
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   5
            Left            =   2160
            TabIndex        =   109
            Top             =   285
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Amount"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   4
            Left            =   720
            TabIndex        =   108
            Top             =   285
            Width           =   1215
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   3
            Left            =   120
            TabIndex        =   107
            Top             =   285
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "End"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   2
            Left            =   2160
            TabIndex        =   99
            Top             =   100
            Width           =   375
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Withdrawal"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   215
            Index           =   1
            Left            =   720
            TabIndex        =   98
            Top             =   100
            Width           =   1215
         End
         Begin Label lblWithdraw 
            Alignment       =   2  'Center
            Caption         =   "Start"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   210
            Index           =   0
            Left            =   120
            TabIndex        =   91
            Top             =   100
            Width           =   375
         End
      End
   End
   Begin Frame fraChange 
      Caption         =   "Future Changes in Death Benefits, Premiums and  Interest Rates "
      FontBold        =   0   'False
      FontItalic      =   0   'False
      FontName        =   "MS Sans Serif"
      FontSize        =   9.75
      FontStrikethru  =   0   'False
      FontUnderline   =   0   'False
      Height          =   2775
      Left            =   240
      TabIndex        =   84
      Top             =   0
      Width           =   9135
      Begin CheckBox chkChangeCurrInt 
         Caption         =   "  Current Interest Rate"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   312
         Left            =   6360
         TabIndex        =   22
         Top             =   240
         Width           =   2532
      End
      Begin Frame fraChangeCurrInt 
         Height          =   2172
         Left            =   6360
         TabIndex        =   97
         Top             =   480
         Width           =   2532
         Begin TextBox txtChangeCurrInt 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   1200
            MaxLength       =   6
            TabIndex        =   32
            Top             =   1800
            Width           =   855
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   1200
            MaxLength       =   6
            TabIndex        =   30
            Top             =   1440
            Width           =   855
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   1200
            MaxLength       =   6
            TabIndex        =   28
            Top             =   1080
            Width           =   855
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   1200
            MaxLength       =   6
            TabIndex        =   26
            Top             =   720
            Width           =   855
         End
         Begin TextBox txtChangeCurrInt 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   1200
            MaxLength       =   6
            TabIndex        =   24
            Top             =   360
            Width           =   855
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   4
            Left            =   360
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   31
            Top             =   1800
            Width           =   372
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   3
            Left            =   360
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   29
            Top             =   1440
            Width           =   372
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   2
            Left            =   360
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   27
            Top             =   1080
            Width           =   372
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   1
            Left            =   360
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   25
            Top             =   720
            Width           =   372
         End
         Begin MaskEdBox txtChangeCurrIntAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   0
            Left            =   360
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   23
            Top             =   360
            Width           =   372
         End
         Begin Label lblChangeCurrInt 
            Alignment       =   2  'Center
            Caption         =   "New Interest Rate"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   840
            TabIndex        =   106
            Top             =   120
            Width           =   1575
         End
         Begin Label lblChangeCurrInt 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   360
            TabIndex        =   92
            Top             =   120
            Width           =   375
         End
      End
      Begin CheckBox chkChangePrem 
         Caption         =   "  Modal Premium Amount"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   312
         Left            =   3240
         TabIndex        =   11
         Top             =   240
         Width           =   2652
      End
      Begin Frame fraChangePrem 
         Height          =   2172
         Left            =   3240
         TabIndex        =   95
         Top             =   480
         Width           =   2652
         Begin TextBox txtChangePrem 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   1080
            MaxLength       =   11
            TabIndex        =   21
            Top             =   1800
            Width           =   1095
         End
         Begin TextBox txtChangePrem 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   1080
            MaxLength       =   11
            TabIndex        =   19
            Top             =   1440
            Width           =   1095
         End
         Begin TextBox txtChangePrem 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   1080
            MaxLength       =   11
            TabIndex        =   17
            Top             =   1080
            Width           =   1095
         End
         Begin TextBox txtChangePrem 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   1080
            MaxLength       =   11
            TabIndex        =   15
            Top             =   720
            Width           =   1095
         End
         Begin TextBox txtChangePrem 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   1080
            MaxLength       =   11
            TabIndex        =   13
            Top             =   360
            Width           =   1095
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   4
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   20
            Top             =   1800
            Width           =   372
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   3
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   18
            Top             =   1440
            Width           =   372
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   2
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   16
            Top             =   1080
            Width           =   372
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   1
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   14
            Top             =   720
            Width           =   372
         End
         Begin MaskEdBox txtChangePremAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   0
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   12
            Top             =   360
            Width           =   372
         End
         Begin Label lblChangePrem 
            Alignment       =   2  'Center
            Caption         =   "New Premium"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   1080
            TabIndex        =   105
            Top             =   120
            Width           =   1095
         End
         Begin Label lblChangePrem 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   480
            TabIndex        =   96
            Top             =   120
            Width           =   375
         End
      End
      Begin CheckBox chkChangeFace 
         Caption         =   "  Specified Death Benefit"
         FontBold        =   0   'False
         FontItalic      =   0   'False
         FontName        =   "MS Sans Serif"
         FontSize        =   8.25
         FontStrikethru  =   0   'False
         FontUnderline   =   0   'False
         Height          =   315
         Left            =   240
         TabIndex        =   0
         Top             =   240
         Width           =   2055
      End
      Begin Frame fraChangeFace 
         Height          =   2172
         Left            =   240
         TabIndex        =   93
         Top             =   480
         Width           =   2652
         Begin TextBox txtChangeFace 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   4
            Left            =   1080
            MaxLength       =   8
            TabIndex        =   10
            Top             =   1800
            Width           =   1095
         End
         Begin TextBox txtChangeFace 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   3
            Left            =   1080
            MaxLength       =   8
            TabIndex        =   8
            Top             =   1440
            Width           =   1095
         End
         Begin TextBox txtChangeFace 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   2
            Left            =   1080
            MaxLength       =   8
            TabIndex        =   6
            Top             =   1080
            Width           =   1095
         End
         Begin TextBox txtChangeFace 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   1
            Left            =   1080
            MaxLength       =   8
            TabIndex        =   4
            Top             =   720
            Width           =   1095
         End
         Begin TextBox txtChangeFace 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   285
            Index           =   0
            Left            =   1080
            MaxLength       =   8
            TabIndex        =   2
            Top             =   360
            Width           =   1095
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   4
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   9
            Top             =   1800
            Width           =   372
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   3
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   7
            Top             =   1440
            Width           =   372
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   2
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   5
            Top             =   1080
            Width           =   372
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   1
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   3
            Top             =   720
            Width           =   372
         End
         Begin MaskEdBox txtChangeFaceAge 
            FontBold        =   0   'False
            FontItalic      =   0   'False
            FontName        =   "MS Sans Serif"
            FontSize        =   8.25
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   288
            Index           =   0
            Left            =   480
            Mask            =   "##"
            MaxLength       =   2
            PromptChar      =   "_"
            TabIndex        =   1
            Top             =   360
            Width           =   372
         End
         Begin Label lblChangeFace 
            Alignment       =   2  'Center
            Caption         =   "New Benefit"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   1
            Left            =   1080
            TabIndex        =   104
            Top             =   120
            Width           =   1095
         End
         Begin Label lblChangeFace 
            Alignment       =   2  'Center
            Caption         =   "Age"
            FontBold        =   -1  'True
            FontItalic      =   0   'False
            FontName        =   "Small Fonts"
            FontSize        =   6.75
            FontStrikethru  =   0   'False
            FontUnderline   =   0   'False
            Height          =   255
            Index           =   0
            Left            =   480
            TabIndex        =   94
            Top             =   120
            Width           =   375
         End
      End
   End
   Begin SSCommand cmdRiders 
      BevelWidth      =   4
      Caption         =   "INSURED &RIDERS"
      Height          =   735
      Left            =   6000
      TabIndex        =   83
      Top             =   6000
      Width           =   1935
   End
   Begin SSCommand cmdPrem 
      BevelWidth      =   4
      Caption         =   "&PREMIUM CALC"
      Height          =   735
      Left            =   3720
      TabIndex        =   82
      Top             =   6000
      Width           =   1935
   End
   Begin SSCommand cmdExit 
      BevelWidth      =   4
      Caption         =   "E&XIT (to CLIENT)"
      Height          =   735
      Left            =   1440
      TabIndex        =   81
      Top             =   6000
      Width           =   1935
   End
End

Sub chkChangeCurrInt_Click ()

    If LSClient(SpouseClient).State = "MI" Then
       If chkChangeCurrInt.Value = 1 Then
          MsgText = "Cannot change Current Interest Rate on LifeSavings illustrations for the state of Michigan"
          MsgBox MsgText, , "LifeSavings"
       End If
       chkChangeCurrInt.Value = 0
    Else
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
    End If

End Sub

Sub chkChangeCurrInt_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkChangeCurrInt_Click

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
         chkLoanRepay.Value = 0
         txtLoanRepayStart(i%).Text = "__"
         txtLoanRepay(i%).Text = ""
         txtLoanRepayEnd(i%).Text = "__"
      End If
    Next i%
    
End Sub

Sub chkLoan_KeyDown (KeyCode As Integer, Shift As Integer)

    Call chkLoan_Click

End Sub

Sub chkLoanRepay_Click ()

    If chkLoanRepay.Value = 1 And chkLoan.Value <> 1 Then
       MsgText = "A loan must exist before creating a Loan Repay."
       MsgBox MsgText, , "LifeSavings"
       chkLoanRepay.Value = 0
       Exit Sub
    End If

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

    lblWithdraw(0).Visible = chkWithDraw.Value
    lblWithdraw(1).Visible = chkWithDraw.Value
    lblWithdraw(2).Visible = chkWithDraw.Value
    lblWithdraw(3).Visible = chkWithDraw.Value
    lblWithdraw(4).Visible = chkWithDraw.Value
    lblWithdraw(5).Visible = chkWithDraw.Value
    For i% = 0 To 4
      txtWithDrawStart(i%).Visible = chkWithDraw.Value
      txtWithdraw(i%).Visible = chkWithDraw.Value
      txtWithdrawEnd(i%).Visible = chkWithDraw.Value
      If chkWithDraw.Value = 0 Then
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
    lblChangeFace(1).Visible = chkChangeFace.Value
    lblChangePrem(1).Visible = chkChangePrem.Value
    lblChangeCurrInt(1).Visible = chkChangeCurrInt.Value
    lblWithdraw(0).Visible = chkWithDraw.Value
    lblWithdraw(1).Visible = chkWithDraw.Value
    lblWithdraw(2).Visible = chkWithDraw.Value
    lblWithdraw(3).Visible = chkWithDraw.Value
    lblWithdraw(4).Visible = chkWithDraw.Value
    lblWithdraw(5).Visible = chkWithDraw.Value
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
      txtWithDrawStart(i%).Visible = chkWithDraw.Value
      txtWithdraw(i%).Visible = chkWithDraw.Value
      txtWithdrawEnd(i%).Visible = chkWithDraw.Value
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
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangeCurrInt(Index).Text)
    txtChangeCurrInt(Index).Text = ""
    txtChangeCurrInt(Index).Text = Clipboard.GetText()
    txtChangeCurrInt(Index).ForeColor = &H80000005
    txtChangeCurrInt(Index).BackColor = &H80000008

End Sub

Sub txtChangeCurrInt_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Then
          chkWithDraw.SetFocus
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
       txtChangeCurrInt(Index).Text = Clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Then
          chkWithDraw.SetFocus
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
    If Val(txtChangeCurrInt(Index).Text) <> 0 And (Val(txtChangeCurrInt(Index).Text) < 3# Or Val(txtChangeCurrInt(Index).Text) > 12#) Then
       MsgText = "Change Interest Rate must be between 3.00% and the Current Interest Rate.  Do not use an Interest Rate that is greater than the Current Rate."
       MsgBox MsgText, , "LifeSavings"
       txtChangeCurrInt(Index).Text = ""
       txtChangeCurrInt(Index).SetFocus
       Exit Sub
    End If
    If LSClient(SpouseClient).CurrIntRate <> 0 And Val(txtChangeCurrInt(Index).Text) > LSClient(SpouseClient).CurrIntRate Then
       MsgText = "Change Interest Rate must be less than or equal to the original Current Interest Rate."
       MsgBox MsgText, , "LifeSavings"
       txtChangeCurrInt(Index).Text = ""
       txtChangeCurrInt(Index).SetFocus
       Exit Sub
    End If
    TempInt = Val(txtChangeCurrInt(Index).Text)
    If TempInt > 0 Then txtChangeCurrInt(Index).Text = Format$(TempInt, "00.000")
    
End Sub

Sub txtChangeCurrIntAge_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangeCurrIntAge(Index).Text)
    txtChangeCurrIntAge(Index).Text = "__"
    txtChangeCurrIntAge(Index).Text = Clipboard.GetText()
    txtChangeCurrIntAge(Index).ForeColor = &H80000005
    txtChangeCurrIntAge(Index).BackColor = &H80000008

End Sub

Sub txtChangeCurrIntAge_KeyDown (Index As Integer, KeyCode As Integer, Shift As Integer)

    If KeyCode = KEY_HOME And Shift = 4 Then chkChangeFace.SetFocus
    If KeyCode = KEY_END And Shift = 4 Then cmdRiders.SetFocus
    If KeyCode = KEY_DOWN Then
       If Index > 3 Or txtChangeCurrIntAge(Index).Text = "__" Then
          chkWithDraw.SetFocus
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
       txtChangeCurrIntAge(Index).Text = Clipboard.GetText()
    End If
    If KeyCode = KEY_RETURN Or KeyCode = KEY_ESCAPE Then
       If Index > 3 Or txtChangeCurrIntAge(Index).Text = "__" Then
          chkLoan.SetFocus
       Else
          txtChangeCurrInt(Index).SetFocus
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_KeyPress (Index As Integer, KeyAscii As Integer)

    If KeyAscii < 48 Or KeyAscii > 57 Then
       KeyAscii = 0
    Else
       If txtChangeCurrIntAge(Index).Text = Clipboard.GetText() Then
          txtChangeCurrIntAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtChangeCurrIntAge(Index).Text) > 9 Then
          Call txtChangeCurrIntAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangeCurrIntAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangeCurrIntAge(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for change must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for change must be less than age 95."
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangeCurrIntAge(Index - 1).Text) Or txtChangeCurrIntAge(Index - 1).Text = "" Then
             MsgText = "Age for change must be greater than previous 'change age'."
          End If
       End If
    Else
       txtChangeCurrInt(Index).Text = ""
       txtChangeCurrIntAge(Index).Text = "__"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtChangeCurrIntAge(Index).Text = "__"
       txtChangeCurrIntAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangeCurrIntAge(Index).ForeColor = &H80000008
    txtChangeCurrIntAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangeCurrInt(Index).SetFocus

End Sub

Sub txtChangeFace_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangeFace(Index).Text)
    txtChangeFace(Index).Text = ""
    txtChangeFace(Index).Text = Clipboard.GetText()
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
       txtChangeFace(Index).Text = Clipboard.GetText()
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
       MsgText = "Change Death Benefit Amount must be between $15,000 and $9,999,999, inclusive."
       MsgBox MsgText, , "LifeSavings"
       txtChangeFace(Index).Text = ""
       txtChangeFace(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtChangeFace(Index).Text)
    If TempValue > 0 Then txtChangeFace(Index).Text = Format$(TempValue, "###00000")

End Sub

Sub txtChangeFaceAge_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangeFaceAge(Index).Text)
    txtChangeFaceAge(Index).Text = "__"
    txtChangeFaceAge(Index).Text = Clipboard.GetText()
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
       txtChangeFaceAge(Index).Text = Clipboard.GetText()
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
       If txtChangeFaceAge(Index).Text = Clipboard.GetText() Then
          txtChangeFaceAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangeFaceAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtChangeFaceAge(Index).Text) > 9 Then
          Call txtChangeFaceAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangeFaceAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangeFaceAge(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for change must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for change must be less than age 95."
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangeFaceAge(Index - 1).Text) Or txtChangeFaceAge(Index - 1).Text = "" Then
             MsgText = "Age for change must be greater than previous 'change age'."
          End If
       End If
    Else
       txtChangeFace(Index).Text = ""
       txtChangeFaceAge(Index).Text = "__"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtChangeFaceAge(Index).Text = "__"
       txtChangeFaceAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangeFaceAge(Index).ForeColor = &H80000008
    txtChangeFaceAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangeFace(Index).SetFocus

End Sub

Sub txtChangePrem_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangePrem(Index).Text)
    txtChangePrem(Index).Text = ""
    txtChangePrem(Index).Text = Clipboard.GetText()
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
       txtChangePrem(Index).Text = Clipboard.GetText()
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
    If Val(txtChangePrem(Index).Text) > 99999999.99 Then
       MsgText = "Change Modal Premium Amount must be less than $99,999,999.99."
       MsgBox MsgText, , "LifeSavings"
       txtChangePrem(Index).Text = ""
       txtChangePrem(Index).SetFocus
       Exit Sub
    End If
    TempPrem = Val(txtChangePrem(Index).Text)
    If TempPrem > 0 Then txtChangePrem(Index).Text = Format$(TempPrem, "###000.00")

End Sub

Sub txtChangePremAge_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtChangePremAge(Index).Text)
    txtChangePremAge(Index).Text = "__"
    txtChangePremAge(Index).Text = Clipboard.GetText()
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
       txtChangePremAge(Index).Text = Clipboard.GetText()
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
       If txtChangePremAge(Index).Text = Clipboard.GetText() Then
          txtChangePremAge(Index).Text = "__"
       End If
    End If

End Sub

Sub txtChangePremAge_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtChangePremAge(Index).Text) > 9 Then
          Call txtChangePremAge_LostFocus(Index)
       End If
    End If

End Sub

Sub txtChangePremAge_LostFocus (Index As Integer)

    chgage% = Val(txtChangePremAge(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for change must be greater than Client issue age."
       End If
       If chgage% >= LSClient(SpouseClient).IssueAge + LSClient(SpouseClient).YearsToPay And LSClient(SpouseClient).YearsToPay <> 0 Then
          MsgText = "Age for change must be less than Client issue age plus years to pay premium."
       End If
       If chgage% > 94 Then
          MsgText = "Age for change must be less than age 95."
       End If
       If Index > 0 Then
          If chgage% <= Val(txtChangePremAge(Index - 1).Text) Or txtChangePremAge(Index - 1).Text = "" Then
             MsgText = "Age for change must be greater than previous 'change age'."
          End If
       End If
    Else
       txtChangePrem(Index).Text = ""
       txtChangePremAge(Index).Text = "__"
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtChangePremAge(Index).Text = "__"
       txtChangePremAge(Index).SetFocus
       Exit Sub
    End If
    
    txtChangePremAge(Index).ForeColor = &H80000008
    txtChangePremAge(Index).BackColor = &H80000005
    If chgage% > 0 Then txtChangePrem(Index).SetFocus

End Sub

Sub txtLoan_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoan(Index).Text)
    txtLoan(Index).Text = ""
    txtLoan(Index).Text = Clipboard.GetText()
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
       txtLoan(Index).Text = Clipboard.GetText()
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
       MsgText = "Loan Amount must be between 100 and 999999, inclusive"
       MsgBox MsgText, , "LifeSavings"
       txtLoan(Index).Text = ""
       txtLoan(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtLoan(Index).Text)
    If TempValue > 0 Then txtLoan(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtLoanEnd_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoanEnd(Index).Text)
    txtLoanEnd(Index).Text = "__"
    txtLoanEnd(Index).Text = Clipboard.GetText()
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
       txtLoanEnd(Index).Text = Clipboard.GetText()
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
       If txtLoanEnd(Index).Text = Clipboard.GetText() Then
          txtLoanEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtLoanEnd(Index).Text) > 9 Then
          Call txtLoanEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanEnd_LostFocus (Index As Integer)

    chgage% = Val(txtLoanEnd(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for loan must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for loan must be less than age 95."
       End If
       If chgage% < Val(txtLoanStart(Index).Text) Or txtLoanStart(Index).Text = "" Then
          MsgText = "Age for Loan End must be greater than or equal to Loan Start age."
       End If
    Else
       If Val(txtLoanStart(Index).Text) > 0 Then
          MsgText = "Age for end of loan MUST be entered."
       Else
          MsgText = ""
       End If
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
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
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoanRepay(Index).Text)
    txtLoanRepay(Index).Text = "__"
    txtLoanRepay(Index).Text = Clipboard.GetText()
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
       txtLoanRepay(Index).Text = Clipboard.GetText()
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
       MsgText = "Loan Repay Amount must be between 10 and 999999, inclusive"
       MsgBox MsgText, , "LifeSavings"
       txtLoanRepay(Index).Text = "__"
       txtLoanRepay(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtLoanRepay(Index).Text)
    If TempValue > 0 Then txtLoanRepay(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtLoanRepayEnd_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoanRepayEnd(Index).Text)
    txtLoanRepayEnd(Index).Text = "__"
    txtLoanRepayEnd(Index).Text = Clipboard.GetText()
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
       txtLoanRepayEnd(Index).Text = Clipboard.GetText()
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
       If txtLoanRepayEnd(Index).Text = Clipboard.GetText() Then
          txtLoanRepayEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanRepayEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtLoanRepayEnd(Index).Text) > 9 Then
          Call txtLoanRepayEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanRepayEnd_LostFocus (Index As Integer)

    chgage% = Val(txtLoanRepayEnd(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for Loan Repay must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for Loan Repay must be less than age 95."
       End If
       If chgage% < Val(txtLoanRepayStart(Index).Text) Or txtLoanRepayStart(Index).Text = "" Then
          MsgText = "Age for Loan Repayment End must be greater than or equal to Loan Repayment Start age."
       End If
    Else
       If Val(txtLoanRepayStart(Index).Text) > 0 Then
          MsgText = "Age for end of loan repayment MUST be entered."
       Else
          MsgText = ""
       End If
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoanRepayStart(Index).Text)
    txtLoanRepayStart(Index).Text = "__"
    txtLoanRepayStart(Index).Text = Clipboard.GetText()
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
       txtLoanRepayStart(Index).Text = Clipboard.GetText()
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
       If txtLoanRepayStart(Index).Text = Clipboard.GetText() Then
          txtLoanRepayStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanRepayStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
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
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for Loan Repay must be greater than Client issue age."
       End If
       If chgage% < loanage% Then
          MsgText = "A loan at an earlier age must exist before creating a Loan Repay."
       End If
       If chgage% > 94 Then
          MsgText = "Age for Loan Repay must be less than age 95."
       End If
       If Index > 0 Then
          TempAge% = Val(txtLoanRepayEnd(Index - 1).Text)
          If chgage% <= Val(txtLoanRepayStart(Index - 1).Text) Or txtLoanRepayStart(Index - 1).Text = "" Or chgage% <= TempAge% Then
             MsgText = "Age for Loan Repay must be greater than age for previous 'Loan Repay period'."
          End If
       End If
    Else
       txtLoanRepayStart(Index).Text = "__"
       txtLoanRepayEnd(Index).Text = "__"
       txtLoanRepay(Index).Text = ""
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtLoanRepayStart(Index).Text = "__"
       txtLoanRepayStart(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanRepayStart(Index).ForeColor = &H80000008
    txtLoanRepayStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtLoanRepay(Index).SetFocus

End Sub

Sub txtLoanStart_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtLoanStart(Index).Text)
    txtLoanStart(Index).Text = "__"
    txtLoanStart(Index).Text = Clipboard.GetText()
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
       txtLoanStart(Index).Text = Clipboard.GetText()
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
       If txtLoanStart(Index).Text = Clipboard.GetText() Then
          txtLoanStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtLoanStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtLoanStart(Index).Text) > 9 Then
          Call txtLoanStart_LostFocus(Index)
       End If
    End If

End Sub

Sub txtLoanStart_LostFocus (Index As Integer)

    chgage% = Val(txtLoanStart(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for Loan must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for Loan must be less than age 95."
       End If
       If Index > 0 Then
          TempAge% = Val(txtLoanEnd(Index - 1).Text)
          If chgage% <= Val(txtLoanStart(Index - 1).Text) Or txtLoanStart(Index - 1).Text = "" Or chgage% <= TempAge% Then
             MsgText = "Age for loan must be greater than age for previous 'loan period'."
          End If
       End If
    Else
       txtLoanStart(Index).Text = "__"
       txtLoanEnd(Index).Text = "__"
       txtLoan(Index).Text = ""
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtLoanStart(Index).Text = "__"
       txtLoanStart(Index).SetFocus
       Exit Sub
    End If
    
    txtLoanStart(Index).ForeColor = &H80000008
    txtLoanStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtLoan(Index).SetFocus

End Sub

Sub txtWithdraw_GotFocus (Index As Integer)
    
    Clipboard.Clear
    Clipboard.SetText Trim$(txtWithdraw(Index).Text)
    txtWithdraw(Index).Text = ""
    txtWithdraw(Index).Text = Clipboard.GetText()
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
          chkWithDraw.SetFocus
       Else
          txtWithdraw(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithdraw(Index).Text = Clipboard.GetText()
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
       MsgText = "Withdraw Amount must be between $100 and $999,999, inclusive."
       MsgBox MsgText, , "LifeSavings"
       txtWithdraw(Index).Text = ""
       txtWithdraw(Index).SetFocus
       Exit Sub
    End If
    TempValue = Val(txtWithdraw(Index).Text)
    If TempValue > 0 Then txtWithdraw(Index).Text = Format$(TempValue, "####000")

End Sub

Sub txtWithdrawEnd_GotFocus (Index As Integer)
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtWithdrawEnd(Index).Text)
    txtWithdrawEnd(Index).Text = "__"
    txtWithdrawEnd(Index).Text = Clipboard.GetText()
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
          chkWithDraw.SetFocus
       Else
          txtWithdrawEnd(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithdrawEnd(Index).Text = Clipboard.GetText()
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
       If txtWithdrawEnd(Index).Text = Clipboard.GetText() Then
          txtWithdrawEnd(Index).Text = "__"
       End If
    End If

End Sub

Sub txtWithdrawEnd_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtWithdrawEnd(Index).Text) > 9 Then
          Call txtWithdrawEnd_LostFocus(Index)
       End If
    End If

End Sub

Sub txtWithdrawEnd_LostFocus (Index As Integer)

    chgage% = Val(txtWithdrawEnd(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for withdraw must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for withdraw must be less than age 95."
       End If
       If chgage% < Val(txtWithDrawStart(Index).Text) Or txtWithDrawStart(Index).Text = "" Then
          MsgText = "Age for Withdrawal End must be greater than or equal to Withdrawal Start."
       End If
    Else
       If Val(txtWithDrawStart(Index).Text) > 0 Then
          MsgText = "Age for end of withdrawals MUST be entered."
       Else
          MsgText = ""
       End If
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
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
 
    Clipboard.Clear
    Clipboard.SetText Trim$(txtWithDrawStart(Index).Text)
    txtWithDrawStart(Index).Text = "__"
    txtWithDrawStart(Index).Text = Clipboard.GetText()
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
          chkWithDraw.SetFocus
       Else
          txtWithDrawStart(Index - 1).SetFocus
       End If
    End If
    If KeyCode = KEY_ESCAPE Then
       txtWithDrawStart(Index).Text = Clipboard.GetText()
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
       If txtWithDrawStart(Index).Text = Clipboard.GetText() Then
          txtWithDrawStart(Index).Text = "__"
       End If
    End If

End Sub

Sub txtWithdrawStart_KeyUp (Index As Integer, KeyCode As Integer, Shift As Integer)

    If (KeyCode > 47 And KeyCode < 58) Or (KeyCode > 95 And KeyCode < 106) Then
       If Val(txtWithDrawStart(Index).Text) > 9 Then
          Call txtWithdrawStart_LostFocus(Index)
       End If
    End If

End Sub

Sub txtWithdrawStart_LostFocus (Index As Integer)

    chgage% = Val(txtWithDrawStart(Index))
    MsgText = ""
    If chgage% <> 0 Then
       TempAge% = LSClient(SpouseClient).IssueAge + 1
       If chgage% < TempAge% Then
          MsgText = "Age for withdraw must be greater than Client issue age."
       End If
       If chgage% > 94 Then
          MsgText = "Age for withdraw must be less than age 95."
       End If
       If Index > 0 Then
          TempAge% = Val(txtWithdrawEnd(Index - 1).Text)
          If chgage% <= Val(txtWithDrawStart(Index - 1).Text) Or txtWithDrawStart(Index - 1).Text = "" Or chgage% <= TempAge% Then
             MsgText = "Age for withdraw must be greater than age for previous 'withdraw period'."
          End If
       End If
    Else
       txtWithDrawStart(Index).Text = "__"
       txtWithdrawEnd(Index).Text = "__"
       txtWithdraw(Index).Text = ""
    End If
    If MsgText <> "" Then
       MsgBox MsgText, , "LifeSavings"
       txtWithDrawStart(Index).Text = "__"
       txtWithDrawStart(Index).SetFocus
       Exit Sub
    End If
    
    txtWithDrawStart(Index).ForeColor = &H80000008
    txtWithDrawStart(Index).BackColor = &H80000005
    If chgage% > 0 Then txtWithdraw(Index).SetFocus

End Sub

