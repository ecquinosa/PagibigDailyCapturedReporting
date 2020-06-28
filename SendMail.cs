
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Report_PagIBIG
{
    class SendMail
    {
               
        public bool SendNotification(string msgBody, string msgSubject2, string fileAttachment, ref string errMsg)
        {
            SmtpClient client = new SmtpClient();
           
            try
            {
                client.Port = Properties.Settings.Default.SMTP_PORT; //587;
                client.Host = Properties.Settings.Default.SMTP_HOST; //"smtp.allcard.com.ph";
               //client.EnableSsl = true;
                client.Timeout = Properties.Settings.Default.SMTP_TIMEOUT; //10000;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTP_USER.Split('@')[0], Properties.Settings.Default.SMTP_PASS);//("ecquinosa", "earl1106_1c");
                client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTP_USER, Properties.Settings.Default.SMTP_PASS);//("ecquinosa", "earl1106_1c");


                MailMessage mm = new MailMessage(Properties.Settings.Default.SMTP_USER, Properties.Settings.Default.SMTP_RECIPIENT, Properties.Settings.Default.BANK.Trim() + " x ALLCARD - REPORT " + DateTime.Now.ToString("MM/dd/yyyy"), msgBody); //("ecquinosa@allcardtech.com.ph", "ecquinosa@allcardtech.com.ph,vsdelafuente@allcardtech.com.ph,agprotacio@allcardtech.com.ph", "test", "test");
                mm.Bcc.Add("ecquinosa@allcardtech.com.ph");
                mm.BodyEncoding = System.Text.UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                Attachment attachment = new Attachment(fileAttachment, System.Net.Mime.MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                //ContentDisposition disposition = data.ContentDisposition;
                //disposition.CreationDate = System.IO.File.GetCreationTime(file);
                //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                // Add the file attachment to this email message.
                mm.Attachments.Add(attachment);                

                client.Send(mm);                

                //System.Windows.Forms.MessageBox.Show("Test message sent. Check your inbox");
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;                
                //Utilities.WriteToRTB(string.Format("SendNotification(): Runtime error {0}", errMsg), ref rtb, ref tssl);
                return false;
            }
            finally
            {
                client = null;
            }
        }

    }
}
