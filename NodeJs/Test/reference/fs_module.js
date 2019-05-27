const fs = require('fs');
const path = require('path');

//create folder
// fs.mkdir(path.join(__dirname,'/test'),{},err=>{
//     if (err) throw err;
//     console.log('folder created');
// });

//create and write to file
fs.writeFile(path.join(__dirname,'/test','hello.txt'),'i am just wastin my time because i have nothing else to do',err=>{
    if (err) throw err;
    console.log('file written');
});

// Append file
fs.appendFile(path.join(__dirname,'/test','hello.txt'),' give me a break',err=>{
    if (err) throw err;
    console.log('file written');
});

// Read file

fs.readFile(path.join(__dirname,'/test','hello.txt'),'utf8',(err,data)=>{
    if (err) throw err;
    console.log(data);
});

//rename file
fs.rename(path.join(__dirname,'/test','hello.txt'),path.join(__dirname,'/test','helloworld.txt'),(err,data)=>{
    if (err) throw err;
    console.log('file renamed');
});