﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MajorasClock
{
    public partial class EditWindow : Form
    {

        public CountdownSettings Settings { get; set; }
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(CountdownSettings settings)
        {
            Settings = settings;
        }
    }
}
