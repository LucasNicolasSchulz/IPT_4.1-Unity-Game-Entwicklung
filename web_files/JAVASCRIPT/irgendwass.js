type="text/javascript"
src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js"

function Button1() {
    const dict_values = {Document} //Pass the javascript variables to a dictionary.
    const s = JSON.stringify(dict_values); // Stringify converts a JavaScript object or value to a JSON string
    console.log(s); // Prints the variables to console window, which are in the JSON format
    fetch("http://127.0.0.1:5000/test1", {
        method:"POST"});
}

function Button2() {
    const dict_values = {Document} //Pass the javascript variables to a dictionary.
    const s = JSON.stringify(dict_values); // Stringify converts a JavaScript object or value to a JSON string
    console.log(s); // Prints the variables to console window, which are in the JSON format
    fetch("http://127.0.0.1:5000/test2", {
        method:"POST"});
}