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
using mintak.Abstractions;

namespace mintak
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
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
            Toy toy=Factory.CreateNew();
            toy.Left = -toy.Width;
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int max = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left>max)
                {
                    max = item.Left;
                }
            }
            if (max>1000)
            {
                Toy elso = _toys[0];
                _toys.Remove(elso);
                mainPanel.Controls.Remove(elso);
            }
        }
    }
}
