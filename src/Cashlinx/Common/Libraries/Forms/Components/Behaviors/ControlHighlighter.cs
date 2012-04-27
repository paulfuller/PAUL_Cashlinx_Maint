using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components.Behaviors
{
    public class ControlHighlighter
    {
        private const int DEFAULT_TIME_BETWEEN_FLASHES = 200;
        private const int DEFAULT_NUMBER_OF_FLASHES = 3;

        public ControlHighlighter(Control control, Color highlightColor)
        {
            Context = new ControlHighlighterContext(control)
            {
                HighlightColor = highlightColor,
                TimesToFlash = DEFAULT_NUMBER_OF_FLASHES,
                TimeBetweenFlashes = DEFAULT_TIME_BETWEEN_FLASHES,
            };
        }

        public ControlHighlighterContext Context { get; set; }

        public void Execute()
        {
            var highlightWorker = new BackgroundWorker();
            highlightWorker.DoWork += highlightWorker_DoWork;
            highlightWorker.RunWorkerAsync(Context);
            highlightWorker.Dispose();
        }

        void highlightWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var context = e.Argument as ControlHighlighterContext;

            while (context.TimesFlashed < context.TimesToFlash)
            {
                if (Context.Control.BackColor == Context.OriginalColor)
                {
                    ChangeControlBackColor(Context.Control, Context.HighlightColor);
                }
                else
                {
                    ChangeControlBackColor(Context.Control, Context.OriginalColor);
                    Context.Flashed();
                }
                System.Threading.Thread.Sleep(Context.TimeBetweenFlashes);
            }
        }

        private void ChangeControlBackColor(Control control, Color color)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() =>
                {
                    control.BackColor = color;
                }));
            }
            else
            {
                control.BackColor = color;
            }
        }

        public class ControlHighlighterContext
        {
            public ControlHighlighterContext(Control control)
            {
                Control = control;
                OriginalColor = control.BackColor;
                TimesFlashed = 0;
            }

            public Control Control { get; private set; }
            public Color HighlightColor { get; set; }
            public int TimesToFlash { get; set; }
            public int TimesFlashed { get; private set; }
            public Color OriginalColor { get; private set; }
            public int TimeBetweenFlashes { get; set; }

            public void Flashed()
            {
                TimesFlashed++;
            }
        }
    }
}
