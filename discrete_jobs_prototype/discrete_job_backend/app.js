const  express = require('express');
const CSVToJSON = require("csvtojson");
const JSONToCSV = require("json2csv");
const FileSyste = require("fs");
const path = require('path');
const cors = require('cors')
var bodyParser = require('body-parser')
 

const app = express()

app.use(function(req, res, next) {
  var allowedOrigins = ['http://127.0.0.1:3000', 'http://localhost:3000', 'https://ad-prototype.herokuapp.com'];
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
app.get('/api', (req, res) => {
  CSVToJSON().fromFile("./source.csv").then(source=>{
      //console.log(req.params)
      //console.log(source);
      return res.status(200).json(source);
  })
  
});
// Priority serve any static files.
app.use(express.static(path.resolve(__dirname, './client/build')));
// All remaining requests return the React app, so it can handle routing.
app.get('/', function(request, response) {
  response.sendFile(path.resolve(__dirname, './client/build', 'index.html'));
});

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }))
 
// parse application/json
app.use(bodyParser.json())




const PORT= process.env.PORT || 5000
app.listen(PORT, () =>
console.log(`Example app listening on port ${PORT}!`),
);