namespace chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tableLayoutRowsAndCollsAutoSize();

        }

        private void tableLayoutRowsAndCollsAutoSize()
        {
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
