const mailer = require('./mailer');
const express = require('express');
const path = require('path');
const cluster = require('cluster');
const sslRedirect = require('heroku-ssl-redirect');
var bodyParser = require("body-parser");
const numCPUs = require('os').cpus().length;

const isDev = process.env.NODE_ENV !== 'production';
const PORT = process.env.PORT || 5000;

const app = express();

app.use(sslRedirect());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

//CORS configuration
app.use(function(req, res, next) {
  var allowedOrigins = ['http://127.0.0.1:3000', 'http://localhost:3000', 'https://poratechai.herokuapp.com', 'www.poratechai.com', 'http://www.poratechai.com', 'https://www.poratechai.com'];
  var origin = req.headers.origin;
  if(allowedOrigins.indexOf(origin) > -1){
      res.setHeader('Access-Control-Allow-Origin', origin);
  }
  // res.header("Access-Control-Allow-Origin", "http://localhost:3000", "https://");
  res.header(
    "Access-Control-Allow-Headers",
    "Origin, X-Requested-With, Content-Type, Accept"
  );
  res.header(
    "Access-Control-Allow-Mehthods",
    "GET, POST, PUT, DELETE, OPTIONS"
  );
  if (req.method == 'OPTIONS') {
    res.end();
  } else {
    next();
  }
});
// Priority serve any static files.
app.use(express.static(path.resolve(__dirname, '../react-ui/build')));

// Answer API requests.
app.get('/api', function (req, res) {
  res.set('Content-Type', 'application/json');
  res.send('{"message":"Hello from the custom server!"}');
});

app.post('/api/send_email', function(req, res) {
  res.set('Content-Type', 'application/json');
  console.log(req.body);
  
  const locals = { userName: req.body.name };
  const messageInfo = {
    email: 'admin@poratechai.com', fromEmail: 'admin@poratechai.com',
    fromName: req.body.name, subject: 'Message from ' + req.body.name, 
    userEmail: req.body.email, message: req.body.message
  };
  mailer.sendOne('contact', messageInfo, locals).then((result) => {
      console.log(result.body)
      res.send(result.body);
  })
  .catch((err) => {
      console.log(err.statusCode)
      res.status(500).send(err);
  })
});


// All remaining requests return the React app, so it can handle routing.
app.get('*', function(request, response) {
  response.sendFile(path.resolve(__dirname, '../react-ui/build', 'index.html'));
});

app.listen(PORT, function () {
  console.error(`Node ${isDev ? 'dev server' : 'cluster worker '+process.pid}: listening on port ${PORT}`);
});
