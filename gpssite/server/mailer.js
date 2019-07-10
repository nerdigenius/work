//server/mailer.js

var path = require("path");
var templatesDir = path.resolve(__dirname, "views/mailer");
var Email = require("email-templates");

const mailjet = require("node-mailjet").connect(
  process.env.MJ_APIKEY_PUBLIC,
  process.env.MJ_APIKEY_PRIVATE
);

const sendEmail = (messageInfo, text, html) => {
  console.log('in sendEmail lalalalalla');
  return mailjet.post("send", { version: "v3.1" }).request({
    Messages: [
      {
        From: { Email: messageInfo.fromEmail, Name: messageInfo.fromName },
        To: [ { Email: messageInfo.email } ],
        Subject: messageInfo.subject,
        // TextPart: text,
        // HTMLPart: html
        TextPart: `Porate Chai Team, You have received a message from a user. User's name - ${messageInfo.fromName}. User email - ${messageInfo.userEmail} Message - ${messageInfo.message}`,
        HTMLPart: `<h3>Porate Chai team,</h3><p>You have received a message from a user. User's name - <strong>${messageInfo.fromName}</strong>. User email - <strong>${messageInfo.userEmail}.</p><p>Message</p><p>${messageInfo.message}</p></strong></p>`
      }
    ]
  });
  
};

exports.sendOne = function(templateName, messageInfo, locals) {
   const email = new Email({
    views: { root: templatesDir, options: { extension: "ejs" } }
  });

  return Promise.all([
    email.render(`${templateName}/html`, locals),
    email.render(`${templateName}/text`, locals)
  ])
    .then(([html, text]) => {
      console.log('in hererererere');
      return sendEmail(messageInfo, text, html);
    })
    .catch(console.error)
    .catch(function(err) {
      console.log('in catch block');
      console.log(err);
    })
};