const url=require('url');

const myurl = new URL('http://mywebsite.com/hello.html?id=100&status=active');

//serialized URL
console.log(myurl.href);

//host(root domain)(does not get port number)
console.log(myurl.hostname);

//pathname
console.log(myurl.pathname);

//Serialized query
console.log(myurl.search);

//params object
console.log(myurl.searchParams);

//add params
myurl.searchParams.append('abc','123');
console.log(myurl.searchParams);

//loop through
myurl.searchParams.forEach((value,name) => console.log(`${name}:${value}`));
