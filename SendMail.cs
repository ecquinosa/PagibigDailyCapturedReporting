
using System;
using System.Collections.Generic;
//using System.Net.Mail;

namespace Report_PagIBIG
{
    class SendMail
    {

        public void Send(string body, string subject, string fileAttachment)
        {
            try
            {
                CreateReport.WriteToLog(string.Format("Start sending of email"));
                var recipients = Properties.Settings.Default.SMTP_RECIPIENT;

                //var recipients = "ecquinosa@allcardtech.com.ph";

                if (recipients != "")
                {
                    recipients = recipients.Replace(";", ",");

                    foreach (string recipient in recipients.Split(','))
                    {
                        SendNotificationv2(body, subject, fileAttachment, recipient);
                    }
                }
                else CreateReport.WriteToLog(string.Format("{0}", "No recipient"));
            }
            catch (Exception ex)
            {
               CreateReport.WriteToLog(string.Format("SendEmail: Runtime error {0}",ex.Message));
            }
        }

        //public bool SendNotification(string msgBody, string msgSubject2, string fileAttachment, ref string errMsg)
        //{
        //    SmtpClient client = new SmtpClient();
           
        //    try
        //    {
        //        client.Port = Properties.Settings.Default.SMTP_PORT; //587;
        //        client.Host = Properties.Settings.Default.SMTP_HOST; //"smtp.allcard.com.ph";
        //       //client.EnableSsl = true;
        //        client.Timeout = Properties.Settings.Default.SMTP_TIMEOUT; //10000;
        //        //client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        //client.UseDefaultCredentials = false;
        //        //client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTP_USER.Split('@')[0], Properties.Settings.Default.SMTP_PASS);//("ecquinosa", "earl1106_1c");
        //        client.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.SMTP_USER, Properties.Settings.Default.SMTP_PASS);//("ecquinosa", "earl1106_1c");


        //        MailMessage mm = new MailMessage(Properties.Settings.Default.SMTP_USER, Properties.Settings.Default.SMTP_RECIPIENT, Properties.Settings.Default.BANK.Trim() + " x ALLCARD - REPORT " + DateTime.Now.ToString("MM/dd/yyyy"), msgBody); //("ecquinosa@allcardtech.com.ph", "ecquinosa@allcardtech.com.ph,vsdelafuente@allcardtech.com.ph,agprotacio@allcardtech.com.ph", "test", "test");
        //        //mm.Bcc.Add("ecquinosa@allcardtech.com.ph");
        //        mm.BodyEncoding = System.Text.UTF8Encoding.UTF8;
        //        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

        //        Attachment attachment = new Attachment(fileAttachment, System.Net.Mime.MediaTypeNames.Application.Octet);
        //        // Add time stamp information for the file.
        //        //ContentDisposition disposition = data.ContentDisposition;
        //        //disposition.CreationDate = System.IO.File.GetCreationTime(file);
        //        //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
        //        //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
        //        // Add the file attachment to this email message.
        //        mm.Attachments.Add(attachment);                

        //        client.Send(mm);                

        //        //System.Windows.Forms.MessageBox.Show("Test message sent. Check your inbox");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        errMsg = ex.Message;                
        //        //Utilities.WriteToRTB(string.Format("SendNotification(): Runtime error {0}", errMsg), ref rtb, ref tssl);
        //        return false;
        //    }
        //    finally
        //    {
        //        client = null;
        //    }
        //}

        public void SendNotificationv2(string body, string subject, string fileAttachment, string recipient)
        {
            var mail = new Allcard.Smtp.Mailkit.Mail();

            try
            {
                mail.SenderEmail = Properties.Settings.Default.SMTP_USER;
                mail.SenderPassword = Properties.Settings.Default.SMTP_PASS;

                mail.SmtpHost = Properties.Settings.Default.SMTP_HOST;
                mail.SmtpPort = Properties.Settings.Default.SMTP_PORT;
                mail.Timeout = Properties.Settings.Default.SMTP_TIMEOUT;

                //List<Allcard.Smtp.Mailkit.Attachment> attachments = null;
                System.Collections.Generic.List<string> attachments = null;
                if (fileAttachment != "")
                {
                    //string fileAttachment = @"D:\megamatcher activation bayambang pc.jpg";
                    attachments = new System.Collections.Generic.List<string>();
                    //var attachment = new Allcard.Smtp.Mailkit.Attachment();
                    //attachment.Filename = Path.GetFileName(fileAttachment);
                    //attachment.Base64 = Convert.ToBase64String(File.ReadAllBytes(fileAttachment));
                    attachments.Add(fileAttachment);
                }

                var isSent = mail.Send(recipient.Trim(), subject, body, attachments);              

                CreateReport.WriteToLog(string.Concat("Allcard.Smtp.Mailkit: ", mail.Logs));

                if (isSent)
                {               
                    CreateReport.WriteToLog(string.Concat("Email report sent to ", recipient));
                }
                else
                {
                    CreateReport.WriteToLog(string.Concat("Email report not sent to ", recipient));
                    CreateReport.WriteToLog(string.Concat("SendEmail(): Failed to send email to ", recipient, ". ", mail.ErrorMessage));                  
                }
            }
            catch (Exception ex)
            {
                CreateReport.WriteToLog(string.Format("SendNotificationv2: Runtime error {0}", ex.ToString()));             
            }
        }

    }
}
