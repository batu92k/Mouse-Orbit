﻿/**
  ******************************************************************************
  * @file    MainForm.cs
  * @author  Ali Batuhan KINDAN
  * @date    17.12.2019
  * @brief   This program performs the quaternion based mouse orbit algorithm. The
  *          app is the re-make of my older mouse orbit implementation that I made 
  *          in 2017 with an older OpenGL API (Tao Framework).
  ******************************************************************************
  */

/*
 *  Mouse Orbit - Quaternion Based Mouse Orbit Algorithm
 *  Copyright (C) 2017, 2019  Ali Batuhan KINDAN
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace Mouse_Orbit
{
    public partial class MainForm : Form
    {
        bool monitorLoaded = false;
        Orbiter orb = new Orbiter();
        Orbiter.Orbit orbitStr;
        bool enableOrbit = false;
        float scaleVal = 1.0f; /* initial scale value for the opengl drawing */

        public MainForm()
        {
            InitializeComponent();
            GL_Monitor.MouseWheel += GL_Monitor_MouseWheel;
        }

        private void GL_Monitor_Load(object sender, EventArgs e)
        {
            monitorLoaded = true;
            GL.ClearColor(Color.Black);
            DrawTimer.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BatuGL.Configure(GL_Monitor);
        }

        private void GL_Monitor_Paint(object sender, PaintEventArgs e)
        {
            if (!monitorLoaded)
                return;

            BatuGL.Configure(GL_Monitor);
            GL.Scale(scaleVal, scaleVal, scaleVal);
            GL.Rotate(orbitStr.angle, orbitStr.ox, orbitStr.oy, orbitStr.oz);
            BatuGL.DrawCube(200);
            GL_Monitor.SwapBuffers();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            orb.Update_Coordinates(MousePosition.X, MousePosition.Y);
            if(enableOrbit) orbitStr = orb.Get_Orbit();
            GL_Monitor.Invalidate();
        }

        private void GL_Monitor_MouseDown(object sender, MouseEventArgs e)
        {
            enableOrbit = (e.Button == MouseButtons.Right);
        }

        private void GL_Monitor_MouseUp(object sender, MouseEventArgs e)
        {
            enableOrbit = (e.Button == MouseButtons.Right) ? false : enableOrbit;
        }

        private void GL_Monitor_MouseWheel(object sender, MouseEventArgs e)
        {
            scaleVal += (e.Delta > 0) ? 0.1f : -0.1f;
            if (scaleVal < 0.1f) scaleVal = 0.1f;
            else if (scaleVal > 5.0f) scaleVal = 5.0f;
        }
    }
}
