const  express = require('express');
const CSVToJSON = require("csvtojson");
const JSONToCSV = require("json2csv");
const FileSyste = require("fs");
const cors = require('cors')
var bodyParser = require('body-parser')
 

const app = express()

app.use(cors())

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }))
 
// parse application/json
app.use(bodyParser.json())


app.get('/', (req, res) => {
    CSVToJSON().fromFile("./source.csv").then(source=>{
        //console.log(req.params)
        return res.status(200).json(source);
    })
    
  });

const PORT= process.env.PORT || 5000
app.listen(PORT, () =>
console.log(`Example app listening on port ${PORT}!`),
);