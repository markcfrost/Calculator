using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Calculator2
{
    public partial class Form1 : Form
    {
        //setting variables
        Double ResultValue = 0;
        Double MemValue = 0;
        String OperationPerformed = "";
        String PasteText = "";
        String FunctionPerformed = "";
        bool IsOperationPerformed = false;
        bool LastAnswer = false;
        bool MemRecall1 = false;
        int BracketCount = 0;
        String Calculation = "";
        String Lastchar = "";
        String string1 = "";
        String string2 = "";
        String string3 = "";
        Double TempCalc = 0;
        DateTime olddate = new DateTime(1, 1, 1);
        DateTime newdate = new DateTime(1, 1, 1);
        String AddCalc = "";


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
            if (Calculation != "")
            {
                if ((Calculation.Substring(Calculation.Length - 1)) != ")")
                    TextResult.Text = TextResult.Text + button.Text;
            }
            else
                TextResult.Text = TextResult.Text + button.Text;

        }

        //Plus Minus Multiply etc
        private void operator1(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            Button button = (Button)sender;
            if (ResultValue != 0 && Calculation == "")
                answer.PerformClick();
            if (Calculation == "")
            {
                OperationPerformed = button.Text;
                ResultValue = double.Parse(TextResult.Text);
                CalcDisplay.Text = ResultValue + " " + OperationPerformed;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
            if (Calculation != "" && AddCalc =="")
            {
                if (TextResult.Text != "0")
                    Calculation = Calculation + TextResult.Text + button.Text;
                else
                    Calculation = Calculation + button.Text;

                CalcDisplay.Text = Calculation;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
            if (Calculation != "" && AddCalc !="")
            {
                if (OperationPerformed == "^")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "˟√")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
                Calculation = Calculation + AddCalc + button.Text;
                AddCalc = "";
                TextResult.Text = "0";
                CalcDisplay.Text = Calculation;
            }
        }
        // Power & Root of...
        private void operator2(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            Button button = (Button)sender;
            if (ResultValue != 0 && Calculation == "")
                answer.PerformClick();
            if (Calculation == "")
            {
                OperationPerformed = button.Text;
                ResultValue = double.Parse(TextResult.Text);
                CalcDisplay.Text = ResultValue + " " + OperationPerformed;
                IsOperationPerformed = true;
                TextResult.Text = "0";
            }
            else //calc if brackets before root/power
            {
                char lastchar = Calculation.Last();
                if (lastchar.Equals(')') && BracketCount == 0)
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
                    CalcDisplay.Text = Calculation + OperationPerformed;
                    TextResult.Text = new DataTable().Compute(AddCalc, null).ToString();
                    AddCalc = TextResult.Text;
                    ResultValue = double.Parse(TextResult.Text);
                    Calculation = Calculation.Substring(0,result2);
                    CalcDisplay.Text = Calculation + AddCalc + OperationPerformed;
                    TextResult.Text = "0";
                    IsOperationPerformed = true;
                }
                if (lastchar.Equals('+') || lastchar.Equals('-') || lastchar.Equals('*') || lastchar.Equals('/'))
                {
                    AddCalc = TextResult.Text;
                    OperationPerformed = button.Text;
                    TextResult.Text = "0";
                }


            }
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

            }
            LastAnswer = true;

        }

        //equals
        private void answer_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            if ((!LastAnswer) && OperationPerformed != "" && Calculation == "")
            {
                CalcDisplay.Text = ResultValue + " " + OperationPerformed + " " + TextResult.Text;
            }
            if (Calculation == "")
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
                    case "^":
                        TextResult.Text = Math.Pow(ResultValue, Double.Parse(TextResult.Text)).ToString();
                        break;
                    case "˟√":
                        CalcDisplay.Text = TextResult.Text + " √ " + ResultValue;
                        TextResult.Text = Math.Pow(ResultValue, 1 / Double.Parse(TextResult.Text)).ToString();
                        break;
                }
                ResultValue = Double.Parse(TextResult.Text);
                OperationPerformed = "";
                MemRecall1 = false;
                LastAnswer = true;
                Calculation = "";
            }
            if (Calculation != "" && BracketCount == 0 && AddCalc=="")
            {//final sum
                if (TextResult.Text != "0")
                    Calculation = Calculation + TextResult.Text;
                CalcDisplay.Text = Calculation;
                TextResult.Text = new DataTable().Compute(Calculation, null).ToString();
                ResultValue = Double.Parse(TextResult.Text);
                Calculation = "";
                LastAnswer = true;
                OperationPerformed = "";
            }
            if (Calculation != "" && BracketCount == 0 && AddCalc != "")
            {
                if (OperationPerformed == "^")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), Double.Parse(TextResult.Text)).ToString();
                if (OperationPerformed == "˟√")
                    AddCalc = Math.Pow(Double.Parse(AddCalc), 1 / Double.Parse(TextResult.Text)).ToString();
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

        private void Square(object sender, EventArgs e)
        {
            if (!IsOperationPerformed)
            {
                CalcDisplay.Text = TextResult.Text + "²";
                TextResult.Text = (Double.Parse(TextResult.Text) * Double.Parse(TextResult.Text)).ToString();
            }
        }

        private void root_Click(object sender, EventArgs e)
        {
            if (!IsOperationPerformed)
            {
                CalcDisplay.Text = "√" + TextResult.Text;
                TextResult.Text = Math.Sqrt(Double.Parse(TextResult.Text)).ToString();
            }
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

        //calculation from string
        private void button10_Click(object sender, EventArgs e)
        {
            string s = "(2+(2*(3*2)))";
            string result3 = "";
            //int result3 = s.Length;
            int result2 = s.LastIndexOf('(',s.Length-2);
            int result1 = s.LastIndexOf(')', s.Length - 2);
            if (result2>result1)
            //string to right of open bracket once logic confirmed
                result3 = s.Substring(result2);
            else
            {
                while (result2 < result1)
                {
                    result2 = s.LastIndexOf('(', result2 - 1);
                    result1 = s.LastIndexOf(')', result1 - 1);
                }
                result3 = s.Substring(0,result2);
            }

            ErrorLabel.Text = result3.ToString();

        }

        private void openbracket_Click(object sender, EventArgs e)
        {
            if (LastAnswer && OperationPerformed == "")
                TextResult.Text = "0";
            if (MemRecall1 && OperationPerformed == "")
                TextResult.Text = "0";

            ErrorLabel.Text = "";
            if (Calculation != "")
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
                        case (")"):
                        case ("("):
                            ErrorLabel.Text = "Can't add open bracket here";
                            break;
                    }
                }
            }
            if (Calculation == "" && OperationPerformed != "")
            {
                Calculation = ResultValue + OperationPerformed + "(";
                IsOperationPerformed = false;
                BracketCount++;
            }
            if (TextResult.Text == "0" && OperationPerformed == "" && Calculation == "")
            {
                Calculation = "(";
                BracketCount++;
            }

            CalcDisplay.Text = Calculation;
        }

        private void closebracket_Click(object sender, EventArgs e)
        {
            ErrorLabel.Text = "";
            if (Calculation != "" && BracketCount > 0)
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
        }
        //test




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
            DPNumber.Visible = false;
            DPLabel.Visible = false;
        }
        //Standard Calc
        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 388;
            TextResult.Width = 329;
            IncDatesCheck.Visible = false;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
        }
        //Scientific Calc
        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 595;
            TextResult.Width = 533;
            DateBox1.Visible = false;
            TempBox.Visible = false;
            Radians.Visible = true;
            Degrees.Visible = true;
            WeightBox.Visible = false;
            IncDatesCheck.Visible = false;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
        }
        //Date Calc
        private void datesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 595;
            TextResult.Width = 533;
            TempBox.Visible = false;
            DateBox1.Visible = true;
            IncDatesCheck.Visible = true;
            WeightBox.Visible = false;
            Radians.Visible = false;
            Degrees.Visible = false;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
        }

        //Temp Calc
        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 595;
            TextResult.Width = 533;
            DateBox1.Visible = false;
            Radians.Visible = false;
            Degrees.Visible = false;
            TempBox.Visible = true;
            WeightBox.Visible = false;
            IncDatesCheck.Visible = false;
            DPNumber.Visible = false;
            DPLabel.Visible = false;
            RecalcTemp.PerformClick();
        }
        //Weight Calc
        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 595;
            TextResult.Width = 533;
            DateBox1.Visible = false;
            Radians.Visible = false;
            Degrees.Visible = false;
            TempBox.Visible = false;
            WeightBox.Visible = true;
            WeightBox.Text = "               Weight               ";
            WeightUnit.Visible = true;
            DistUnit.Visible = false;
            DPNumber.Visible = true;
            DPLabel.Visible = true;
            IncDatesCheck.Visible = false;
            RecalcTemp.PerformClick();
        }
        //Distance Calc
        private void distanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 595;
            TextResult.Width = 533;
            DateBox1.Visible = false;
            Radians.Visible = false;
            Degrees.Visible = false;
            TempBox.Visible = false;
            WeightBox.Visible = true;
            WeightBox.Text = "              Distance              ";
            WeightUnit.Visible = false;
            DistUnit.Visible = true;
            DPNumber.Visible = true;
            DPLabel.Visible = true;
            IncDatesCheck.Visible = false;
            RecalcTemp.PerformClick();
        }
        //Testing setting
        private void testingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 1000;
            TextResult.Width = 533;
        }

        private void DistChange(object sender, EventArgs e)
        {
            DistanceRefresh();
        }

        public void WeightCalc(object sender, EventArgs e)
        {
            WeightRefresh();
        }

        public void Recalc(object sender, EventArgs e)
        {
            if (WeightUnit.Visible)
                WeightRefresh();
            if (DistUnit.Visible)
                DistanceRefresh();
        }

        public void WeightRefresh()
        {
            LastAnswer = true;
            if (WeightUnit.Text == "Kilogram (kg)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.157473;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 2.204623;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 35.27396;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " oz";
            }
            if (WeightUnit.Text == "Gram (g)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.000157473;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 0.002204623;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 0.03527396;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " oz";
            }
            if (WeightUnit.Text == "Stone (st.)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 6.350293;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 14;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " lb";
                TempCalc = Double.Parse(TextResult.Text) * 224;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " oz";
            }
            if (WeightUnit.Text == "Pounds (lb)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.453592;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 0.071429;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " stone";
                TempCalc = Double.Parse(TextResult.Text) * 16;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " oz";
            }
            if (WeightUnit.Text == "Ounces (oz)")
            {
                TempCalc = Double.Parse(TextResult.Text) * 28.34952;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " g";
                TempCalc = Double.Parse(TextResult.Text) * 0.02834952;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " kg";
                TempCalc = Double.Parse(TextResult.Text) * 0.0625;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " lb";
            }
        }
        public void DistanceRefresh()
        {
            LastAnswer = true;
            if (DistUnit.Text == "Miles")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1.609344;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " km";
                TempCalc = Double.Parse(TextResult.Text) * 1760;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 5280;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " feet";
            }
            if (DistUnit.Text == "Yards")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.9144;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " metres";
                TempCalc = Double.Parse(TextResult.Text) * 3;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 36;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " inches";
            }
            if (DistUnit.Text == "Feet")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.3048;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " metres";
                TempCalc = Double.Parse(TextResult.Text) / 3;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 12;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " inches";
            }
            if (DistUnit.Text == "Inches")
            {
                TempCalc = Double.Parse(TextResult.Text) * 2.54;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " cm";
                TempCalc = Double.Parse(TextResult.Text) * 0.083333;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 0.27778;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " yards";
            }
            if (DistUnit.Text == "Kilometres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.621371;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " miles";
                TempCalc = Double.Parse(TextResult.Text) * 1093.613;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 1000;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " metres";
            }
            if (DistUnit.Text == "Metres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 1.093613;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 3.28084;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 39.37008;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " inches";
            }
            if (DistUnit.Text == "Centimetres")
            {
                TempCalc = Double.Parse(TextResult.Text) * 0.01093613;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight1.Text = TempCalc.ToString() + " yards";
                TempCalc = Double.Parse(TextResult.Text) * 0.0328084;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight2.Text = TempCalc.ToString() + " feet";
                TempCalc = Double.Parse(TextResult.Text) * 0.3937008;
                TempCalc = Math.Round(TempCalc, (int)DPNumber.Value);
                Weight3.Text = TempCalc.ToString() + " inches";
            }
        }


    }
}


