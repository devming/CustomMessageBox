using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * CustomMessageBox
 * 터치화면에서 메시지박스 버튼 크기 조절을 위해 다시 만든 메시지 박스 다이얼로그
 */

namespace CustomMessageBox
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        public CustomMessageBox(string title, string question, MessageBoxKind field)
        {
            InitializeComponent();
            this.Text = title;
            this.questionLabel.Text = question;
            this.mBoxKind = field;

            if (field == MessageBoxKind.Default)
            {
                confirmButton.Visible = false;
                noButton.Visible = false;
                cancelButton.Text = "확인";
            }
            else if (field == MessageBoxKind.YesNo)
            {
                confirmButton.Visible = true;
                noButton.Visible = false;
                confirmButton.Text = "예";
                cancelButton.Text = "아니요";
            }
            else // field == MessageBoxKind.YesNoCancel
            {
                confirmButton.Visible = true;
                noButton.Visible = true;
                confirmButton.Text = "확인";
                cancelButton.Text = "취소";
            }
            // TODO: questionLabel에 한글28자까지 쓰면 다음줄로. <<- 이 기능은 보류. 일단 필요없을듯.
        }

        public enum MessageBoxKind
        {
            Default, YesNo, YesNoCancel 
        }

        public enum DialogResults
        {
            No, Yes, Cancel
        }

        private MessageBoxKind mBoxKind;
        private static DialogResults response;

        private void confirmButton_Click(object sender, EventArgs e)
        {
            setResponse(DialogResults.Yes);
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            setResponse(DialogResults.No);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (mBoxKind == MessageBoxKind.YesNo)       // YesNo버튼일 때는 No기능 담당. 레이아웃 위치상 noButton을 표시하면 너무 왼쪽으로 쏠리는 느낌이라 cancel버튼으로 no기능 대체함.
                setResponse(DialogResults.No);
            else // (mBoxKind == MessageBoxKind.YesNoCancel) || (mBoxKind == MessageBoxKind.Default)    
                setResponse(DialogResults.Cancel);

            this.Close();
        }

        public void setResponse(DialogResults res)
        {
            response = res;
        }

        public static DialogResults getResponse()
        {
            return response;
        }

    }
}
