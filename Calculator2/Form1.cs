using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Calculator2
{
    public partial class Form1 : Form
    {
        //setting variables
        Double ResultValue = 0;
        Double MemValue = 0;
        String OperationPerformed = "";
        String OperationPerformed2 = "";
        String PasteText = "";
        String FunctionPerformed = "";
        bool IsOperationPerformed = false;
        bool LastAnswer = false;
        bool MemRecall1 = false;
        int BracketCount = 0;
        int BracketCount2 = 0;
        String Calculation = "";
        String Lastchar = "";
        String string1 = "";
        String string2 = "";
        String string3 = "";
        Double TempCalc = 0;
        DateTime olddate = new DateTime(1, 1, 1);
        DateTime newdate = new DateTime(1, 1, 1);
        String AddCalc = "";
        String AddCalc2 = "";


        public Form1()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            FlowLayoutPanel panel = new FlowLayoutPanel();
            panel.AutoSize = true;
            panel.FlowDirection = FlowDirection.TopDown;

            this.Controls.Add(panel);
            this.KeyPreview = true;
            this.KeyPress +=
                new KeyPressEventHandler(Form1_KeyPress);
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 40 && e.KeyChar <= 57)
            {


                switch (e.KeyChar)
                {
                    case (char)40:
                        openbracket.PerformClick();
                        break;
                    case (char)41:
                        closebracket.PerformClick();
                        break;
                    case (char)42:
                        mulitply.PerformClick();
                        break;
                    case (char)43:
                        plus.PerformClick();
                        break;
                    case (char)45:
                        minus.PerformClick();
                        break;
                    case (char)46:
                        button_dp.PerformClick();
                        break;
                    case (char)47:
                        divide.PerformClick();
                        break;
                    case (char)48:
                        button0.PerformClick();
                        break;
                    case (char)49:
                        button1.PerformClick();
                        break;
                    case (char)50:
                        button2.PerformClick();
                        break;
                    case (char)51:
                        button3.PerformClick();
                        break;
                    case (char)52:
                        button4.PerformClick();
                        break;
                    case (char)53:
                        button5.PerformClick();
                        break;
                    case (char)54:
                        button6.PerformClick();
                        break;
                    case (char)55:
                        button7.PerformClick();
                        break;
                    case (char)56:
                        button8.PerformClick();
                        break;
                    case (char)57:
                        button9.PerformClick();
                        break;
                }
            }
            if (e.KeyChar == 8)
                BackSpace.PerformClick();
            if (e.KeyChar == 61 || e.KeyChar == 13)
                answer.PerformClick();
            if (e.KeyChar == 27)
                ButtonCE.PerformClick();
            e.Handled = true;
            ErrorLabel.Focus();
            }


        //Numbers
        private void button_click(object sender, EventArgs e)
        {
            //reset if equals pressed so not adding characters to result
            ErrorLabel.Text = "";
            if ((LastAnswer) || (MemRecall1))
            {
                TextResult.Text = "0";
                CalcDisplay.Text = "";
                LastAnswer = false;
                MemRecall1 = false;
            }
            if ((TextResult.Text == "0") || IsOperationPerformed)
                TextResult.Clear();
            Button button = (Button)sender;
            IsOperationPerformed = false;
            if (button.Text == ".")
            {
                if (!TextResult.Text.Contains("."))
                    TextResult.Text = TextResult.Text + button.Text;
                if (TextResult.Text.Substring(0, 1) == ".")
                    TextResult.Text = "0.";
            }
            if (button.Text != ".")
                TextResult.Text = TextResult.Text + button.Text;

        }

        //Plus Minus Multiply etc
        private void operator1(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            Button button = (Button)sender;
            if (ResultValue != 0 && Calculation == "" && AddCalc == "")
                answer.PerformClick();
            if (Calculation == "" && AddCalc == "")
            {
                OperationPerformed = button.Text;
                if (button.Text == "Mod")
                    OperationPerformed = "%";
                ResultValue = double.Parse(TextResult.Text);
                CalcDisplay.Text = ResultValue + " " + OperationPerformed;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
            if (Calculation != "" && AddCalc == "")
            {
                OperationPerformed = button.Text;
                if (button.Text == "Mod")
                    OperationPerformed = "%";
                if (TextResult.Text != "0")
                    Calculation = Calculation + TextResult.Text + OperationPerformed;
                else
                    Calculation = Calculation + OperationPerformed;

                CalcDisplay.Text = Calculation;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
            if (AddCalc !="" && AddCalc2=="")
            {
                if (OperationPerformed == "xʸ")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "ʸ√x")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "Exp")
                    AddCalc = (Double.Parse(AddCalc) * Math.Pow(10, Double.Parse(TextResult.Text))).ToString();
                if (button.Text != "Mod")
                    Calculation = Calculation + AddCalc + button.Text;
                else
                    Calculation = Calculation + AddCalc + "%";
                AddCalc = "";
                TextResult.Text = "0";
                CalcDisplay.Text = Calculation;
                IsOperationPerformed = true;
            }
            if (AddCalc2 !="")
            {
                if (button.Text != "Mod")
                    AddCalc2 = AddCalc2 + TextResult.Text + button.Text;
                else
                    AddCalc2 = AddCalc2 + TextResult.Text + "%";
                CalcDisplay.Text = Calculation + AddCalc + OperationPerformed2 + AddCalc2;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
        }
        // Power & Root of & Exp...
        private void operator2(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            Button button = (Button)sender;
            if (AddCalc != "")
            {
                if (OperationPerformed == "xʸ")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "ʸ√x" && !AddCalc.Contains("-"))
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "ʸ√x" && AddCalc.Contains("-"))
                    ErrorLabel.Text = "Invalid Calculation";
                if (OperationPerformed == "Exp")
                    AddCalc = (Double.Parse(AddCalc) * Math.Pow(10, Double.Parse(TextResult.Text))).ToString();
                OperationPerformed = button.Text;
                TextResult.Text = "0";
                CalcDisplay.Text = Calculation + AddCalc + button.Text;
                IsOperationPerformed = true;
            }

            if (ResultValue != 0 && Calculation == "" && AddCalc =="")
                answer.PerformClick();
            if (Calculation == "" && AddCalc == "")
            {
                OperationPerformed = button.Text;
                if (TextResult.Text.Contains("-") && OperationPerformed == "ʸ√x")
                {
                    OperationPerformed = "";
                    ErrorLabel.Text = "Invalid Calculation";
                }
                else
                { 
                ResultValue = double.Parse(TextResult.Text);
                CalcDisplay.Text = ResultValue + " " + OperationPerformed;
                IsOperationPerformed = true;
                TextResult.Text = "0";
                }
            }
            if (Calculation != "" && AddCalc == "") //calc if brackets before root/power
            {
                char lastchar = Calculation.Last();
                if (lastchar.Equals(')'))
                {

                    int result2 = Calculation.LastIndexOf('(', Calculation.Length - 2);
                    int result1 = Calculation.LastIndexOf(')', Calculation.Length - 2);
                    //if (result2 > result1)

                   // finds section of calculation string to sum for power/root calc
                        while (result2 < result1)
                        {
                            result2 = Calculation.LastIndexOf('(', result2 - 1);
                            result1 = Calculation.LastIndexOf(')', result1 - 1);
                        }
                    AddCalc = Calculation.Substring(result2);
                    OperationPerformed = button.Text;
                    string1 = new DataTable().Compute(AddCalc, null).ToString();
                    if (OperationPerformed == "ʸ√x" && string1.Contains("-"))
                    {
                        ErrorLabel.Text = "Invalid Operator on Negative Number";
                        OperationPerformed = "";
                    }
                    else
                    {
                    AddCalc = string1;
                    ResultValue = double.Parse(TextResult.Text);
                    Calculation = Calculation.Substring(0,result2);
                    CalcDisplay.Text = Calculation + AddCalc + OperationPerformed;
                    TextResult.Text = "0";
                    IsOperationPerformed = true;
                    }
                    string1 = "";
                }
                if (lastchar.Equals('('))
                {
                    OperationPerformed = button.Text;
                    if (OperationPerformed == "ʸ√x" && TextResult.Text.Contains("-"))
                    {
                        ErrorLabel.Text = "Invalid Operator on Negative Number";
                        OperationPerformed = "";
                    }
                    else
                    { 
                    AddCalc = TextResult.Text;
                    CalcDisplay.Text = Calculation + AddCalc + OperationPerformed;
                    TextResult.Text = "0";
                    IsOperationPerformed = true;
                    }
                }
                if (lastchar.Equals('+') || lastchar.Equals('-') || lastchar.Equals('*') || lastchar.Equals('/'))
                {
                    OperationPerformed = button.Text;
                    if (OperationPerformed == "ʸ√x" && TextResult.Text.Contains("-"))
                    {
                        ErrorLabel.Text = "Invalid Operator on Negative Number";
                        OperationPerformed = "";
                    }
                    else
                    {
                        AddCalc = TextResult.Text;
                        CalcDisplay.Text = Calculation + AddCalc + OperationPerformed;
                        TextResult.Text = "0";
                        IsOperationPerformed = true;
                    }
                }
            }
            if (AddCalc2 != "")
                ErrorLabel.Text = "Complete Brackets First";
            
        }
       //Functions
        private void function_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            FunctionPerformed = button.Text;
            CalcDisplay.Text = FunctionPerformed + " " + TextResult.Text;
            switch (FunctionPerformed)
            {
                case "sin":
                    if (Radians.Checked)
                        TextResult.Text = Math.Sin(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Sin((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    break;
                case "cos":
                    if (Radians.Checked)
                        TextResult.Text = Math.Cos(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Cos((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    break;
                case "tan":
                    if (Radians.Checked)
                        TextResult.Text = Math.Tan(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Tan((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    break;
                case "sin¯¹":
                    if (Radians.Checked)
                        TextResult.Text = Math.Asin(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Asin((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    if (TextResult.Text == "NaN")
                    {
                        ErrorLabel.Text = "Invalid Calculation";
                        TextResult.Text = "0";
                    }
                    break;
                case "cos¯¹":
                    if (Radians.Checked)
                        TextResult.Text = Math.Acos(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Acos((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    if (TextResult.Text == "NaN")
                    {
                        ErrorLabel.Text = "Invalid Calculation";
                        TextResult.Text = "0";
                    }
                    break;
                case "tan¯¹":
                    if (Radians.Checked)
                        TextResult.Text = Math.Atan(Double.Parse(TextResult.Text)).ToString();
                    if (Degrees.Checked)
                        TextResult.Text = Math.Atan((Math.PI * Double.Parse(TextResult.Text)) / 180).ToString();
                    break;
                case "10˟":
                    TextResult.Text = Math.Pow(10, Double.Parse(TextResult.Text)).ToString();
                    break;
                case "e˟":
                    TextResult.Text = Math.Exp(Double.Parse(TextResult.Text)).ToString();
                    break;
                case "log":
                    TextResult.Text = Math.Log10(Double.Parse(TextResult.Text)).ToString();
                    break;
                case "ln":
                    TextResult.Text = Math.Log(Double.Parse(TextResult.Text)).ToString();
                    break;
                case "n!":
                    if (TextResult.Text.Contains("-"))
                    {
                        ErrorLabel.Text = "Number must be positive";
                        CalcDisplay.Text = "";
                        LastAnswer = true;
                    }
                    else
                    {
                        CalcDisplay.Text = TextResult.Text + "!";
                        if (Double.Parse(TextResult.Text) % 1 == 0)
                            TextResult.Text = SpecialFunctions.Factorial(Int64.Parse(TextResult.Text)).ToString();
                        else
                            TextResult.Text = SpecialFunctions.Gamma(double.Parse(TextResult.Text) + 1).ToString();
                    }
                    break;
                case "%":
                    CalcDisplay.Text = TextResult.Text + "%";
                    TextResult.Text = (Double.Parse(TextResult.Text) / 100).ToString();
                    break;
                case "√":
                    CalcDisplay.Text = "√" + TextResult.Text;
                    TextResult.Text = Math.Sqrt(Double.Parse(TextResult.Text)).ToString();
                    if (TextResult.Text == "NaN")
                        {
                            ErrorLabel.Text = "Invalid Operation on Neg Number";
                            TextResult.Text = "0";
                        }
                    break;
                case "x²":
                    CalcDisplay.Text = TextResult.Text + "²";
                    TextResult.Text = (Double.Parse(TextResult.Text) * Double.Parse(TextResult.Text)).ToString();
                    break;
            }
            LastAnswer = true;

        }

        //equals
        private void answer_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            if ((!LastAnswer) && OperationPerformed != "" && Calculation == ""  && AddCalc2=="")
            {
                CalcDisplay.Text = ResultValue + " " + OperationPerformed + " " + TextResult.Text;
            }
            if (Calculation == "" && AddCalc2 == "")
            {
                switch (OperationPerformed)

                {
                    case "+":
                        TextResult.Text = (ResultValue + Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "-":
                        TextResult.Text = (ResultValue - Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "*":
                        TextResult.Text = (ResultValue * Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "/":
                        TextResult.Text = (ResultValue / Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "xʸ":
                        TextResult.Text = Math.Pow(ResultValue, Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "Mod":
                        TextResult.Text = (ResultValue % Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "%":
                        TextResult.Text = (ResultValue % Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "ʸ√x":
                        CalcDisplay.Text = TextResult.Text + " √ " + ResultValue;
                        TextResult.Text = Math.Pow(ResultValue, 1 / Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "Exp":
                        CalcDisplay.Text = ResultValue + " E " + TextResult.Text ;
                        TextResult.Text = (ResultValue * Math.Pow(10, Double.Parse(TextResult.Text))).ToString();
                        break;
                }
                if (TextResult.Text=="NaN")
                {
                    ErrorLabel.Text = "Invalid Calculation";
                    TextResult.Text = "0";
                }
                ResultValue = Double.Parse(TextResult.Text);
                OperationPerformed = "";
                MemRecall1 = false;
                LastAnswer = true;
                Calculation = "";
            }
            if (Calculation != "" && BracketCount == 0 && AddCalc == "")
            {
                //final sum
                if (TextResult.Text != "0")
                    Calculation = Calculation + TextResult.Text;
                CalcDisplay.Text = Calculation;
                TextResult.Text = new DataTable().Compute(Calculation, null).ToString();
                ResultValue = Double.Parse(TextResult.Text);
                Calculation = "";
                LastAnswer = true;
                OperationPerformed = "";
            }
            if (Calculation != "" && BracketCount == 0 && AddCalc != ""  && AddCalc2 == "")
            {
                if (OperationPerformed == "xʸ")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "ʸ√x")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "Exp")
                    AddCalc = (Double.Parse(AddCalc) * Math.Pow(10, Double.Parse(TextResult.Text))).ToString();
                Calculation = Calculation + AddCalc;
                CalcDisplay.Text = Calculation;
                TextResult.Text = new DataTable().Compute(Calculation, null).ToString();
                ResultValue = Double.Parse(TextResult.Text);
                Calculation = "";
                LastAnswer = true;
                OperationPerformed = "";
                AddCalc = "";
            }
            if (Calculation != "" && BracketCount > 0)
            {
                ErrorLabel.Text = "Complete the Calculation (Brackets)";
                TextResult.Text = "0";
            }
            if (AddCalc2 != "" && BracketCount > 0)
            {
                ErrorLabel.Text = "Complete the Calculation (Brackets)";
            }

        }
        //Clear All
        private void ButtonCE_Click(object sender, EventArgs e)
        {
            TextResult.Text = "0";
            ResultValue = 0;
            Calculation = "";
            BracketCount = 0;
            CalcDisplay.Text = "";
            ErrorLabel.Text = "";
            OperationPerformed2 = "";
            AddCalc = "";
            AddCalc2 = "";
            binary.Text = "";
            octal.Text = "";
            hexa.Text = "";

        }

        //Clear Text Box
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            TextResult.Text = "0";
            ErrorLabel.Text = "";
        }

        private void ValuePi(object sender, EventArgs e)
        {
            TextResult.Text = Math.PI.ToString();
            LastAnswer = false;
            MemRecall1 = true;
        }

        //Switch sign of number displayed
        private void NegSign(object sender, EventArgs e)
        {
            if ((TextResult.Text != "0") && TextResult.Text.Substring(0, 1) != "-")
            {
                TextResult.Text = "-" + TextResult.Text;
            }
            else
            {
                TextResult.Text = TextResult.Text.Substring(1);
            }

        }

        private void MemAdd_Click(object sender, EventArgs e)
        {
            MemValue = MemValue + Double.Parse(TextResult.Text);
            CalcDisplay.Text = "Memory Value: " + MemValue.ToString();

            LastAnswer = true;

        }

        private void MemSub_Click(object sender, EventArgs e)
        {
            MemValue = MemValue - Double.Parse(TextResult.Text);
            CalcDisplay.Text = "Memory Value: " + MemValue.ToString();
            LastAnswer = true;

        }

        private void MemClear_Click(object sender, EventArgs e)
        {
            MemValue = 0;
            CalcDisplay.Text = "Memory Value: " + MemValue.ToString();
        }

        private void MemRecall_Click(object sender, EventArgs e)
        {
            //CalcDisplay.Text = "Memory Value: " + MemValue.ToString();
            TextResult.Text = MemValue.ToString();
            LastAnswer = false;
            MemRecall1 = true;
        }

        private void BackSpce1(object sender, EventArgs e)
        {
            if (TextResult.TextLength > 0)
            {
                TextResult.Text = TextResult.Text.Remove(TextResult.Text.Length - 1, 1);
            }
            if (TextResult.Text == "")
                TextResult.Text = "0";
        }

        private void inverse_Click(object sender, EventArgs e)
        {
            TextResult.Text = (1 / Double.Parse(TextResult.Text)).ToString();
        }

        //Copy Value to Clipboard
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(TextResult.Text);
        }
        //Cut Value to Clipboard
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(TextResult.Text);
            TextResult.Text = "0";
        }
        //Check Clipboard has numeric value & Paste in
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if paste will be decimal
            PasteText = System.Windows.Forms.Clipboard.GetText();
            decimal PasteCheck;
            if (Decimal.TryParse(PasteText, out PasteCheck))
            {
                TextResult.Text = PasteText;
                MemRecall1 = true;
            }
        }

        private void e_Click(object sender, EventArgs e)
        {
            TextResult.Text = Math.E.ToString();
            LastAnswer = false;
            MemRecall1 = true;
        }

        //Open Bracket loop
        private void openbracket_Click(object sender, EventArgs e)
        {
            if (LastAnswer && OperationPerformed == "")
                TextResult.Text = "0";
            if (MemRecall1 && OperationPerformed == "")
                TextResult.Text = "0";

            ErrorLabel.Text = "";
            if (Calculation != "" && OperationPerformed != "xʸ" && OperationPerformed != "ʸ√x" && OperationPerformed != "Exp" && AddCalc2=="")
            {
                if (TextResult.Text == "0")
                {
                    Lastchar = Calculation.Substring(Calculation.Length - 1);
                    switch (Lastchar)
                    {
                        case ("("):
                        case ("+"):
                        case ("-"):
                        case ("*"):
                        case ("/"):
                        case ("%"):
                            {
                                Calculation = Calculation + "(";
                                BracketCount++;
                            }
                            break;
                        case (")"):
                            ErrorLabel.Text = "Can't add open bracket here";
                            break;
                    }
                }
                else
                {
                    Lastchar = Calculation.Substring(Calculation.Length - 1);
                    switch (Lastchar)
                    {
                        case ("+"):
                        case ("-"):
                        case ("*"):
                        case ("/"):
                        case ("%"):
                        case (")"):
                        case ("("):
                            ErrorLabel.Text = "Can't add open bracket here";
                            break;
                    }
                }
            }
            if (OperationPerformed == "xʸ" || OperationPerformed == "ʸ√x" || OperationPerformed == "Exp" || AddCalc2 != "")
            {
                if (AddCalc == "")
                    AddCalc = ResultValue.ToString();
                if (AddCalc2 == "")
                { 
                    AddCalc2 = "(";
                    BracketCount++;
                    BracketCount2++;
                    OperationPerformed2 = OperationPerformed;
                }
                if (TextResult.Text == "0" && AddCalc2!="(")
                {
                    Lastchar = AddCalc2.Substring(AddCalc2.Length - 1);
                    switch (Lastchar)
                    {
                        case ("("):
                        case ("+"):
                        case ("-"):
                        case ("*"):
                        case ("/"):
                        case ("%"):
                            {
                                AddCalc2 = AddCalc2 + "(";
                                BracketCount++;
                                BracketCount2++;
                            }
                            break;
                        case (")"):
                            ErrorLabel.Text = "Can't add open bracket here";
                            break;
                    }
                }
                if (TextResult.Text != "0" && AddCalc2 !="(")
                {
                    Lastchar = AddCalc2.Substring(AddCalc2.Length - 1);
                    switch (Lastchar)
                    {
                        case ("+"):
                        case ("-"):
                        case ("*"):
                        case ("/"):
                        case ("%"):
                        case (")"):
                        case ("("):
                            ErrorLabel.Text = "Can't add open bracket here";
                            break;
                    }
                }
            }
            if (Calculation == "" && OperationPerformed != ""  && AddCalc2=="")
            {
                Calculation = ResultValue + OperationPerformed + "(";
                IsOperationPerformed = false;
                BracketCount++;
            }
            if (TextResult.Text == "0" && OperationPerformed == "" && Calculation == "" && AddCalc2=="")
            {
                Calculation = "(";
                BracketCount++;
            }
            if (AddCalc2 == "")
                CalcDisplay.Text = Calculation;
            else
                CalcDisplay.Text = Calculation + AddCalc + OperationPerformed2 + AddCalc2;
        }
        //close bracket
        private void closebracket_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            if (Calculation != "" && BracketCount > 0 && AddCalc == "")
            {
                Lastchar = Calculation.Substring(Calculation.Length - 1);
                if (Lastchar == "+" || Lastchar == "-" || Lastchar == "*" || Lastchar == "/" || Lastchar == ")" || Lastchar == "(")
                {
                    if (Lastchar == ")")
                        Calculation = Calculation + ")";
                    if (Lastchar != ")")
                        Calculation = Calculation + TextResult.Text + ")";
                    ErrorLabel.Text = "";
                    CalcDisplay.Text = Calculation;
                    TextResult.Text = "0";
                    BracketCount--;
                }
                else
                    ErrorLabel.Text = "Unable to close braket here";
            }
            else
                ErrorLabel.Text = "Unable to close braket here";
            if (BracketCount >0 && AddCalc != "" && AddCalc2 =="")
            {
                if (OperationPerformed == "xʸ")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "ʸ√x")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "Exp")
                    AddCalc = (Double.Parse(AddCalc) * Math.Pow(10, Double.Parse(TextResult.Text))).ToString();
                Calculation = Calculation + AddCalc + ")";
                AddCalc = "";
                TextResult.Text = "0";
                CalcDisplay.Text = Calculation;
                BracketCount--;
                ErrorLabel.Text = "";
                IsOperationPerformed = true;
            }
            if (AddCalc2 != "" && BracketCount > 0)
            {
                Lastchar = AddCalc2.Substring(AddCalc2.Length - 1);
                if (Lastchar == "+" || Lastchar == "-" || Lastchar == "*" || Lastchar == "/" || Lastchar == ")" || Lastchar == "(")
                {
                    if (Lastchar == ")")
                        AddCalc2 = AddCalc2 + ")";
                    if (Lastchar != ")")
                        AddCalc2 = AddCalc2 + TextResult.Text + ")";
                    ErrorLabel.Text = "";
                    CalcDisplay.Text = Calculation + AddCalc + OperationPerformed2 + AddCalc2;
                    TextResult.Text = "0";
                    BracketCount--;
                    BracketCount2--;
                    if (BracketCount2 == 0)
                    {

                        if (OperationPerformed2 == "xʸ")
                        {
                            AddCalc2 = new DataTable().Compute(AddCalc2, null).ToString();
                            AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(AddCalc2)).ToString();
                        }
                        if (OperationPerformed2 == "ʸ√x")
                        {
                            AddCalc2 = new DataTable().Compute(AddCalc2, null).ToString();
                            AddCalc = Math.Pow(Double.Parse(AddCalc), 1/Double.Parse(AddCalc2)).ToString();
                        }
                        if (OperationPerformed2 == "Exp")
                        {
                            AddCalc2 = new DataTable().Compute(AddCalc2, null).ToString();
                            AddCalc = (Double.Parse(AddCalc) * Math.Pow(10, Double.Parse(AddCalc2))).ToString();
                        }
                        if (Calculation == "") 
                        {
                            TextResult.Text = AddCalc;
                            AddCalc = "";
                            AddCalc2 = "";
                            OperationPerformed2 = "";
                            ResultValue = Double.Parse(TextResult.Text);
                            OperationPerformed = "";
                            LastAnswer = true;
                        }
                        if (Calculation != "")
                        {
                            Calculation = Calculation + AddCalc;
                            AddCalc = "";
                            AddCalc2 = "";
                            OperationPerformed2 = "";
                            OperationPerformed = "";
                            TextResult.Text = "0";
                        }
                            
                    }

                }
            }
        }


        private void DateValueChanged(object sender, EventArgs e)
        {
            if (Date1.Value.Date < Date2.Value.Date && IncDatesCheck.Checked)
                DaysOnly.Text = ((Date2.Value.Date.AddDays(1) - Date1.Value.Date).TotalDays).ToString() + " Days";
            if (Date1.Value.Date == Date2.Value.Date && IncDatesCheck.Checked)
                DaysOnly.Text = ((Date2.Value.Date.AddDays(1) - Date1.Value.Date).TotalDays).ToString() + " Day";
            if (Date1.Value.Date > Date2.Value.Date && IncDatesCheck.Checked)
                DaysOnly.Text = ((Date1.Value.Date.AddDays(1) - Date2.Value.Date).TotalDays).ToString() + " Days";
            if (Date1.Value.Date < Date2.Value && !IncDatesCheck.Checked)
                DaysOnly.Text = ((Date2.Value.Date - Date1.Value.Date).TotalDays).ToString() + " Days";
            if (Date1.Value.Date == Date2.Value.Date && !IncDatesCheck.Checked)
                DaysOnly.Text = ((Date2.Value.Date - Date1.Value.Date).TotalDays).ToString() + " Day";
            if (Date1.Value.Date > Date2.Value.Date && !IncDatesCheck.Checked)
                DaysOnly.Text = ((Date1.Value.Date - Date2.Value.Date).TotalDays).ToString() + " Days";

            DateTime zeroTime = new DateTime(1, 1, 1);
            if (Date1.Value.Date > Date2.Value.Date)
            {
                olddate = Date2.Value.Date;
                newdate = Date1.Value.Date;
            }
            if (Date1.Value.Date <= Date2.Value.Date)
            {
                olddate = Date1.Value.Date;
                newdate = Date2.Value.Date;
            }
            if (IncDatesCheck.Checked)
            {
                newdate = newdate.AddDays(1);
            }

            TimeSpan span = newdate - olddate;

            // because we start at year 1 for the Gregorian 
            // calendar, we must subtract a year here.

            int years = (zeroTime + span).Year - 1;
            int months = (zeroTime + span).Month - 1;
            int days = (zeroTime + span).Day - 1;

            //plural or singular
            if (years == 1 || years == -1)
                string1 = " year";
            else
                string1 = " years";
            if (months == 1 || months == -1)
                string2 = " month";
            else
                string2 = " months";
            if (days == 1 || days == -1)
                string3 = " day";
            else
                string3 = " days";


            if (years > 0)
                DaysCalc.Text = years.ToString() + string1 + Environment.NewLine + months.ToString() + string2 + Environment.NewLine + days.ToString() + string3;
            if (years == 0 && months > 0)
                DaysCalc.Text = months.ToString() + string2 + Environment.NewLine + days.ToString() + string3;
            if (years == 0 && months == 0)
                DaysCalc.Text = days.ToString() + string3;

        }
        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            LastAnswer = true;
            if (TempUnit.Text == "Fahrenheit (°F)")
            {
                TempCalc = Double.Parse(TextResult.Text);
                TempCalc = TempCalc - 32;
                TempCalc = TempCalc * 0.55555555555555555555555555555555555;
                TempCalc = Math.Round(TempCalc, 2);
                Temp1.Text = TempCalc.ToString() + "°C";
                TempCalc = TempCalc + 273.15;
                Temp2.Text = TempCalc.ToString() + "K";
            }
            if (TempUnit.Text == "Celsius (°C)")
            {
                TempCalc = Double.Parse(TextResult.Text);
                TempCalc = TempCalc * 1.8;
                TempCalc = TempCalc + 32;
                TempCalc = Math.Round(TempCalc, 2);
                Temp1.Text = TempCalc.ToString() + "°F";
                TempCalc = Double.Parse(TextResult.Text) + 273.15;
                TempCalc = Math.Round(TempCalc, 2);
                Temp2.Text = TempCalc.ToString() + "K";

            }
            if (TempUnit.Text == "Kelvin (K)")
            {
                TempCalc = Double.Parse(TextResult.Text);
                TempCalc = TempCalc - 273.15;
                TempCalc = Math.Round(TempCalc, 2);
                Temp1.Text = TempCalc.ToString() + "°C";
                TempCalc = TempCalc * 1.8;
                TempCalc = TempCalc + 32;
                TempCalc = Math.Round(TempCalc, 2);
                Temp2.Text = TempCalc.ToString() + "°F";

            }
            TempUnit.Select(0, 0);
        }

        //load screen width
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 388;
            TextResult.Width = 329;
            openbracket.Enabled = true;
            closebracket.Enabled = true;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
            DateBox1.Visible = false;
            TempBox.Visible = false;
            Radians.Visible = false;
            VolUnit.Visible = false;
            Degrees.Visible = false;
            WeightBox.Visible = false;
            IncDatesCheck.Visible = false;
            Programmer.Visible = false;
            ReEnableButtons();
        }
        //Standard Calc
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 388;
            TextResult.Width = 329;
            HideAll();
            ReEnableButtons();
        }
        //Scientific Calc
        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 655;
            TextResult.Width = 589;
            HideAll();
            Radians.Visible = true;
            Degrees.Visible = true;
            ReEnableButtons();
        }
        //Date Calc
        private void datesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 840;
            TextResult.Width = 533;
            HideAll();
            TimeBox.Visible = true;
            DateBox1.Visible = true;
            IncDatesCheck.Visible = true;
            Arrow.Visible = true;
            TimeCalc();
            ReEnableButtons();
            StopBrackets();
        }

        //Temp Calc
        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 580;
            TextResult.Width = 533;
            HideAll();
            TempBox.Visible = true;
            RecalcTemp.PerformClick();
            ReEnableButtons();
            StopBrackets();
        }
        //Weight Calc
        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 580;
            TextResult.Width = 533;
            HideAll();
            WeightBox.Visible = true;
            WeightBox.Text = "               Weight               ";
            WeightUnit.Visible = true;
            DPNumber.Visible = true;
            DPLabel.Visible = true;
            WeightRefresh();
            ReEnableButtons();
            StopBrackets();
        }
        //Distance Calc
        private void distanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 580;
            TextResult.Width = 533;
            HideAll();
            WeightBox.Visible = true;
            WeightBox.Text = "              Distance              ";
            DistUnit.Visible = true;
            DPNumber.Visible = true;
            DPLabel.Visible = true;
            DistanceRefresh();
            ReEnableButtons();
            StopBrackets();
        }
        //Volume Calc
        private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 580;
            TextResult.Width = 533;
            HideAll();
            WeightBox.Visible = true;
            WeightBox.Text = "              Volume              ";
            VolUnit.Visible = true;
            DPNumber.Visible = true;
            DPLabel.Visible = true;
            VolumeRefresh();
            ReEnableButtons();
            StopBrackets();
        }
        //Binary, Octal & Hex calc
        private void binaryOctHexaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 580;
            TextResult.Width = 533;
            HideAll();
            Programmer.Visible = true;
            DisableButtons();
        }

        public void HideAll()
        {
            IncDatesCheck.Visible = false;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
            TempBox.Visible = false;
            Radians.Visible = false;
            Degrees.Visible = false;
            VolUnit.Visible = false;
            DistUnit.Visible = false;
            WeightUnit.Visible = false;
            WeightBox.Visible = false;
            Programmer.Visible = false;
            IncDatesCheck.Visible = false;
            TimeBox.Visible = false;
            Arrow.Visible = false;
        }

        private void DistChange(object sender, EventArgs e)
        {
            DistanceRefresh();
        }

        public void WeightCalc(object sender, EventArgs e)
        {
            WeightRefresh();
        }

        public void VolCalc(object sender, EventArgs e)
        {
            VolumeRefresh();
        }

        public void Recalc(object sender, EventArgs e)
        {
            if (WeightUnit.Visible)
                WeightRefresh();
            if (DistUnit.Visible)
                DistanceRefresh();
            if (VolUnit.Visible)
                VolumeRefresh();

        }

        public void WeightRefresh()
        {
            LastAnswer = true;
            if (WeightUnit.Text == "Kilogram (kg)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.157473;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 2.204623;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 35.27396;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " oz";
            }
            if (WeightUnit.Text == "Gram (g)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.000157473;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 0.002204623;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 0.03527396;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " oz";
            }
            if (WeightUnit.Text == "Stone (st.)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 6.350293;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 14;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 224;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " oz";
            }
            if (WeightUnit.Text == "Pounds (lb)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.453592;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 0.071429;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 16;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " oz";

            }
            if (WeightUnit.Text == "Ounces (oz)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 28.34952;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " g";
                TempCalc = Double.Parse(TextResult.Text) * 0.02834952;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 0.0625;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " lb";
            }
        }
        public void DistanceRefresh()
        {
            LastAnswer = true;
            if (DistUnit.Text == "Miles")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1.609344;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " km";
                TempCalc = Double.Parse(TextResult.Text) * 1760;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 5280;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " feet";
            }
            if (DistUnit.Text == "Yards")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.9144;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " metres";
                TempCalc = Double.Parse(TextResult.Text) * 3;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 36;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " inches";
            }
            if (DistUnit.Text == "Feet")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.3048;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " metres";
                TempCalc = Double.Parse(TextResult.Text) / 3;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 12;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " inches";
            }
            if (DistUnit.Text == "Inches")
            {
                TempCalc = Double.Parse(TextResult.Text) * 2.54;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " cm";
                TempCalc = Double.Parse(TextResult.Text) * 0.083333;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 0.27778;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
            }
            if (DistUnit.Text == "Kilometres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.621371;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " miles";
                TempCalc = Double.Parse(TextResult.Text) * 1093.613;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 1000;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " metres";
            }
            if (DistUnit.Text == "Metres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1.093613;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 3.28084;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 39.37008;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " inches";
            }
            if (DistUnit.Text == "Centimetres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.01093613;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 0.0328084;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 0.3937008;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " inches";
            }
        }
        public void VolumeRefresh()
        {
            LastAnswer = true;
            if (VolUnit.Text == "Millilitres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.168936;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Tsp";
                TempCalc = Double.Parse(TextResult.Text) * 0.056312;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Tbsp";
                TempCalc = Double.Parse(TextResult.Text) * 0.035195;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " fl oz";
            }
            if (VolUnit.Text == "Litres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 35.19508;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " fl oz";
                TempCalc = Double.Parse(TextResult.Text) * 1.759754;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Pints";
                TempCalc = Double.Parse(TextResult.Text) * 0.879877;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Quarts";
            }
            if (VolUnit.Text == "Cubic Metres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1759.754;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Pints";
                TempCalc = Double.Parse(TextResult.Text) * 219.9692;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Gallons";
                TempCalc = Double.Parse(TextResult.Text) * 35.31467;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Cubic Feet";
            }
            if (VolUnit.Text == "Teaspoons")
            {
                TempCalc = Double.Parse(TextResult.Text) * 5.919388;
                Weight1.Text = string.Format("{0:n"+(int)DPNumber.Value+"}",TempCalc) + " ml";
                TempCalc = Double.Parse(TextResult.Text) /3;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Tbsp";
                TempCalc = Double.Parse(TextResult.Text) * 0.2083333333;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " fl oz";
            }
            if (VolUnit.Text == "Tablespoons")
            {
                TempCalc = Double.Parse(TextResult.Text) * 17.75816;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " ml";
                TempCalc = Double.Parse(TextResult.Text) * 0.625;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " fl oz";
                TempCalc = Double.Parse(TextResult.Text) * 3;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " tsp";
            }
            if (VolUnit.Text == "Fluid Ounces")
            {
                TempCalc = Double.Parse(TextResult.Text) * 28.41306;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " ml";
                TempCalc = Double.Parse(TextResult.Text) * 1.6;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Tbsp";
                TempCalc = Double.Parse(TextResult.Text) * 0.05;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Pints";
            }
            if (VolUnit.Text == "Pints")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.568261;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " l";
                TempCalc = Double.Parse(TextResult.Text) * 20;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " fl oz";
                TempCalc = Double.Parse(TextResult.Text) * 0.125;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Gallons";
            }
            if (VolUnit.Text == "Quarts")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1.136523;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " l";
                TempCalc = Double.Parse(TextResult.Text) * 2;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Pints";
                TempCalc = Double.Parse(TextResult.Text) * 0.25;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Gallons";
            }
            if (VolUnit.Text == "Gallons")
            {
                TempCalc = Double.Parse(TextResult.Text) * 4.54609;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " l";
                TempCalc = Double.Parse(TextResult.Text) * 8;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Pints";
                TempCalc = Double.Parse(TextResult.Text) * 0.160544;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Cubic Feet";
            }
            if (VolUnit.Text == "Cubic Feet")
            {
                TempCalc = Double.Parse(TextResult.Text) * 28.31685;
                Weight1.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " l";
                TempCalc = Double.Parse(TextResult.Text) * 0.02831685;
                Weight2.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Cubic Metres";
                TempCalc = Double.Parse(TextResult.Text) * 6.228835;
                Weight3.Text = string.Format("{0:n" + (int)DPNumber.Value + "}", TempCalc) + " Gallons";
            }

        }
            private void Random_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            var numero = rnd.NextDouble();
            TextResult.Text = numero.ToString();
            LastAnswer = false;
            MemRecall1 = true;
        }

        private void Hexidecimal_Click(object sender, EventArgs e)
        {
            Hexadecimal();
        }

        public void Hexadecimal()
        {
            if (TextResult.Text.Contains("."))
            {
                ErrorLabel.Text = "Must Be a Whole Number";
                binary.Text = "";
                octal.Text = "";
                hexa.Text = "";
            }
            else
            {
                if (Math.Abs(Int64.Parse(TextResult.Text)) < 1000000000000000)
                {
                    string binary1 = Convert.ToString(Int64.Parse(TextResult.Text), 2);
                    binary.Text = binary1;
                    string octal1 = Convert.ToString(Int64.Parse(TextResult.Text), 8);
                    octal.Text = octal1;
                    hexa.Text = string.Format("{0:x}", (Int64.Parse(TextResult.Text)));
                }
                else
                {
                    ErrorLabel.Text = "Number to Large";
                    binary.Text = "";
                    octal.Text = "";
                    hexa.Text = "";
                }
            }
            LastAnswer = true;

        }
        public void DisableButtons()
        {
            button_dp.Enabled = false;
            inverse.Enabled = false;
            openbracket.Enabled = false;
            closebracket.Enabled = false;
        }
        public void ReEnableButtons()
        {
            button_dp.Enabled = true;
            inverse.Enabled = true;
            openbracket.Enabled = true;
            closebracket.Enabled = true;
        }
        public void StopBrackets()
        {
            openbracket.Enabled = false;
            closebracket.Enabled = false;
        }

        private void TimeRecalc_Click(object sender, EventArgs e)
        {
            TimeCalc();
        }

        public void TimeCalc()
        {
            if (TimeUnit1.Text == TimeUnit2.Text)
                TempCalc = Double.Parse(TextResult.Text);
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) * 52.17857;
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) * 365.25;
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) * 8766;
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) * 525960;
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) * 31557600;
            if (TimeUnit1.Text == "Years" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 31557600000;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) * 0.019165;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) * 7;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) * 168;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) * 10080;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) * 604800;
            if (TimeUnit1.Text == "Weeks" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 604800000;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) / 365.25;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) / 7;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) * 24;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) * 1440;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) * 86400;
            if (TimeUnit1.Text == "Days" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 86400000;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) / 8766;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) / 168;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) / 24;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) * 60;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) * 3600;
            if (TimeUnit1.Text == "Hours" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 3600000;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) / 525960;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) / 10080;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) / 1440;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) / 60;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) * 60;
            if (TimeUnit1.Text == "Minutes" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 60000;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) / 31557600;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) / 604800;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) / 86400;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) / 3600;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) /60 ;
            if (TimeUnit1.Text == "Seconds" && TimeUnit2.Text == "Milliseconds")
                TempCalc = Double.Parse(TextResult.Text) * 100;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Years")
                TempCalc = Double.Parse(TextResult.Text) / 31557600000;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Weeks")
                TempCalc = Double.Parse(TextResult.Text) / 604800000;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Days")
                TempCalc = Double.Parse(TextResult.Text) / 86400000;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Hours")
                TempCalc = Double.Parse(TextResult.Text) / 3600000;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Minutes")
                TempCalc = Double.Parse(TextResult.Text) / 60000;
            if (TimeUnit1.Text == "Milliseconds" && TimeUnit2.Text == "Seconds")
                TempCalc = Double.Parse(TextResult.Text) / 100;

            if (TempCalc >= 1)
                TimeResult.Text = string.Format("{0:n0}", TempCalc) + " " + TimeUnit2.Text;
            else
                TimeResult.Text = TempCalc.ToString() + " " + TimeUnit2.Text;
        }
    }
    
}


