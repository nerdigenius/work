const path = require ('path');

//Base file name
console.log(path.basename(__filename));

//Direcotry name
console.log(__dirname);

//File Extension
console.log(path.extname(__filename));

// Create path Object
console.log(path.parse(__filename).base);


//concatenate path
console.log(path.join(__dirname,'test','hello.html'));