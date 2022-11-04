using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mintak.Features;

namespace mintak
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Ball ball=Factory.CreateNew();
            ball.Left = -ball.Width;
            _balls.Add(ball);
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int max = 0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (item.Left>max)
                {
                    max = item.Left;
                }
            }
            if (_balls.Count>0&&_balls[0].Left>1000)
            {
                //Ball elso = _balls[0];
                _balls.Remove(_balls[0]);
                mainPanel.Controls.Remove(_balls[0]);
            }
        }
    }
}
