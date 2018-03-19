using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace SistemaVentas.Presentacion
{
    /// <summary>
    /// Un textBox que no admite carcateres especiales como ´'|
    /// </summary>
    public class TextBoxPersonal : TextBox
    {
        public enum Vista
        {
            Normal,
            Error
        }
        private Vista myVista;
        /// <summary>
        /// Determina el tipo de dato permitido en el pca
        /// </summary>
        public enum Tipo
        {
            /// <summary>
            /// Permitir Caracteres Especiales
            /// </summary>
            StringEspeciales,

            /// <summary>
            /// No Permitit Caracteres Especiales
            /// </summary>
            StringNoEspeciales,

            /// <summary>
            /// Permitir solo Numeros Enteros
            /// </summary>
            Enteros,

            /// <summary>
            /// Permite solo Numeros Enteros Positivos
            /// </summary>
            EnterosPositivos,

            /// <summary>
            /// Permitir solo Numeros Decimales
            /// </summary>
            Decimales,

            /// <summary>
            /// Permitir solo Numeros Decimales EU separador decimal punto (.)
            /// </summary>
            DecimalesEEUU
        }

        [Description("Determina el tipo de dato que se puede introducir en el Texbox")]
        [Category("Datos")]
        [DefaultValue(Tipo.StringEspeciales)]
        public Tipo TipoDato { get; set; }

        char[] digitosEnteros = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '\b' };

        char[] digitosEnterosPositivos = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\b' };

        char[] digitosDecimales     = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', ',', '-', '\b' };
        char[] digitosDecimalesEEUU = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '\b' };

        char[] digitosEspeciales = new char[] { (char)(34), (char)(35), (char)(36), (char)(39), (char)(43) };

        protected virtual bool CaracterCorrecto(Char c)
        {
            switch (TipoDato)
            {
                case Tipo.Decimales:
                    return !(Array.IndexOf(digitosDecimales, c) == -1);
                case Tipo.DecimalesEEUU:
                    return !(Array.IndexOf(digitosDecimalesEEUU, c) == -1);
                case Tipo.Enteros:
                    return !(Array.IndexOf(digitosEnteros, c) == -1);
                case Tipo.EnterosPositivos:
                    return !(Array.IndexOf(digitosEnterosPositivos, c) == -1);
                case Tipo.StringEspeciales:
                    return true;
                case Tipo.StringNoEspeciales:
                    return (Array.IndexOf(digitosEspeciales, c) == -1);
                default:
                    return false;
            }
        }

        #region Propiedades de apariencia

        public Vista Apariencia
        {
            get
            {
                return myVista;
            }
            set
            {
                this.myVista = value;
                CambiarVista();
            }
        }

        private void CambiarVista()
        {
            switch (myVista)
            {
                case Vista.Error:
                    this.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
                    this.ForeColor = System.Drawing.Color.FromName("Window");
                    break;
                case Vista.Normal:
                    this.BackColor = System.Drawing.Color.FromName("Window");
                    this.ForeColor = System.Drawing.Color.FromName("WindowText");
                    break;
            }
        }


        #endregion

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!CaracterCorrecto(e.KeyChar))
                e.Handled = true;
            base.OnKeyPress(e);
        }

        /*
        protected override void OnTextChanged(EventArgs e)
        {
            this.Apariencia = Vista.Normal;   
            base.OnTextChanged(e);
        }
        */

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                string s = "";
                foreach (char c in value)
                {
                    if (CaracterCorrecto(c))
                        s += c;
                }
                base.Text = s;
            }
        }
    }
}