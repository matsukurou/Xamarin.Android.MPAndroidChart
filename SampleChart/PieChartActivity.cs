
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;

using MikePhil.Charting.Charts;
using MikePhil.Charting.Listener;
using MikePhil.Charting.Animation;
using MikePhil.Charting.Components;
using MikePhil.Charting.Data;
using MikePhil.Charting.Util;
using MikePhil.Charting.Formatter;
using MikePhil.Charting.Highlight;

namespace SampleChart
{
    [Activity(Label = "PieChartActivity", MainLauncher = true, Icon = "@drawable/icon")]
    //[Activity(Label = "PieChartActivity")]
    public class PieChartActivity : Activity, Android.Widget.SeekBar.IOnSeekBarChangeListener, IOnChartValueSelectedListener
    {
        protected String[] mMonths = new String[] {
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
        };

        protected String[] mParties = new String[] {
            "Party A", "Party B", "Party C", "Party D", "Party E", "Party F", "Party G", "Party H",
            "Party I", "Party J", "Party K", "Party L", "Party M", "Party N", "Party O", "Party P",
            "Party Q", "Party R", "Party S", "Party T", "Party U", "Party V", "Party W", "Party X",
            "Party Y", "Party Z"
        };

        PieChart mChart;
        SeekBar mSeekBarX, mSeekBarY;
        TextView tvX, tvY;

        private Typeface tf;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            SetContentView(Resource.Layout.PieChartLayout);

            //mChart = new PieChart(this);
            //SetContentView(mChart);

            #if true
            tvX = (TextView) FindViewById(Resource.Id.tvXMax);
            tvY = (TextView) FindViewById(Resource.Id.tvYMax);

            mSeekBarX = (SeekBar) FindViewById(Resource.Id.seekBar1);
            mSeekBarY = (SeekBar) FindViewById(Resource.Id.seekBar2);

            mSeekBarY.Progress = 10;

            mSeekBarX.SetOnSeekBarChangeListener(this);
            mSeekBarY.SetOnSeekBarChangeListener(this);
            #endif
            #if true
            mChart = (PieChart) FindViewById(Resource.Id.chart1);
            mChart.SetUsePercentValues(true);
            mChart.SetDescription("");
            mChart.SetExtraOffsets(5, 10, 5, 5);

            mChart.DragDecelerationFrictionCoef = 0.95f;

            //tf = Typeface.CreateFromAsset(Assets, "OpenSans-Regular.ttf");

            //mChart.SetCenterTextTypeface(Typeface.CreateFromAsset(Assets, "OpenSans-Light.ttf"));
            mChart.CenterText = "MPAndroidChart developed by Philipp Jahoda";
            //mChart.setcenter CenterText = new SpannableString(mChart.) Spanable generateCenterSpannableText();

            mChart.DrawHoleEnabled = true;
            mChart.SetHoleColorTransparent(true);

            mChart.SetTransparentCircleColor(Color.White);
            mChart.SetTransparentCircleAlpha(110);

            mChart.HoleRadius = 58f;
            mChart.TransparentCircleRadius = 61f;

            mChart.SetDrawCenterText(true);

            mChart.RotationAngle = 0;
            // enable rotation of the chart by touch
            mChart.RotationEnabled = true;
            mChart.HighlightEnabled = true;

            // mChart.setUnit(" €");
            // mChart.setDrawUnitsInChart(true);

            // add a selection listener
            mChart.SetOnChartValueSelectedListener(this);

            SetData(3, 100);

            mChart.AnimateY(1400, Easing.EasingOption.EaseInOutQuad);
            // mChart.spin(2000, 0, 360);

            var l = mChart.Legend;
            l.Position = Legend.LegendPosition.RightOfChart;
            l.XEntrySpace = 7f;
            l.YEntrySpace = 0f;
            l.YOffset = 0f;
            // Create your application here
            #endif
        }

        #if false
        SpannableString generateCenterSpannableText()
        {
            SpannableString s = new SpannableString("MPAndroidChart developed by Philipp Jahoda");
            s.SetSpan(new RelativeSizeSpan(1.7f), 0, 14, 0);
            s.SetSpan(new StyleSpan(TypefaceStyle.Normal), 14, s.Length() - 15, 0);
            s.SetSpan(new ForegroundColorSpan(Color.Gray), 14, s.Length() - 15, 0);
            s.SetSpan(new RelativeSizeSpan(.8f), 14, s.Length() - 15, 0);
            s.SetSpan(new StyleSpan(TypefaceStyle.Italic), s.Length() - 14, s.Length(), 0);
            s.SetSpan(new ForegroundColorSpan(ColorTemplate.HoloBlue), s.Length() - 14, s.Length(), 0);
            return s;
        }
        #endif

        void SetData(int count, float range)
        {
            float mult = range;

            var yVals1 = new List<Entry>();

            // IMPORTANT: In a PieChart, no values (Entry) should have the same
            // xIndex (even if from different DataSets), since no values can be
            // drawn above each other.
            for (int i = 0; i < count + 1; i++) {
                yVals1.Add(new Entry((float) (new Random().Next() * mult) + mult / 5, i));
            }

            var xVals = new List<String>();

            for (int i = 0; i < count + 1; i++)
                xVals.Add(mParties[i % mParties.Length]);

            PieDataSet dataSet = new PieDataSet(yVals1, "Election Results");
            dataSet.SliceSpace = 2f;
            dataSet.SelectionShift = 5f;

            var colors = new List<Java.Lang.Integer>();

            foreach (Java.Lang.Integer c in ColorTemplate.VordiplomColors)
                colors.Add(c);

            foreach (Java.Lang.Integer c in ColorTemplate.JoyfulColors)
                colors.Add(c);

            foreach (Java.Lang.Integer c in ColorTemplate.ColorfulColors)
                colors.Add(c);

            foreach (Java.Lang.Integer c in ColorTemplate.LibertyColors)
                colors.Add(c);

            foreach (Java.Lang.Integer c in ColorTemplate.PastelColors)
                colors.Add(c);

            colors.Add((Java.Lang.Integer)ColorTemplate.HoloBlue);

            dataSet.Colors = colors;
            //dataSet.setSelectionShift(0f);

            PieData data = new PieData(xVals, dataSet);
            data.SetValueFormatter(new PercentFormatter());
            data.SetValueTextSize(11f);
            data.SetValueTextColor(Color.White);
            data.SetValueTypeface(tf);
            mChart.Data = data;

            // undo all highlights
            mChart.HighlightValues(null);

            mChart.Invalidate();
        }

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser) {

            tvX.Text = ("" + (mSeekBarX.Progress + 1));
            tvY.Text = ("" + (mSeekBarY.Progress));

            SetData(mSeekBarX.Progress, mSeekBarY.Progress);
        }

        public void OnValueSelected(Entry e, int dataSetIndex, HighlightBase h) {

            if (e == null)
                return;
            //Log.i("VAL SELECTED", "Value: " + e.Val + ", xIndex: " + e.XIndex + ", DataSet index: " + dataSetIndex);
        }

        public void OnNothingSelected() {
            //Log.i("PieChart", "nothing selected");
        }

        public void OnStartTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }

        public void OnStopTrackingTouch(SeekBar seekBar) {
            // TODO Auto-generated method stub

        }
    }
}

