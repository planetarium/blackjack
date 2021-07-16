function request(url, method, body, loader){
    process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
    var XMLHttpRequest = require("xmlhttprequest").XMLHttpRequest;
    var httpRequest = new XMLHttpRequest();

    function responseResponse(){
        if (httpRequest.readyState === httpRequest.DONE) {
            loader(httpRequest.responseText);
        }
    }

    httpRequest.open(method, "https://localhost:5001/" + url, false);
    httpRequest.onreadystatechange = responseResponse;
    httpRequest.send(body);
}
var privatekey = "";
var address = "";
var blockHeight = 0;
var moneyEarned;
var moneyCumulated;
var cumulatingSince;
var randomSeed;
var gamestatus = 0;
function standby(){
    request("game/standby", "POST", "", function(text){
        privatekey = text;
    })
    request("game/address/" + privatekey, "GET", "", function(text){
        address = text;
    })
    request("block/height", "GET", "", function(text){
        blockHeight = text;
    })
}

function start(){
    request("game/start", "POST", "{\"privateKey\": " + privatekey + "}", function(text){
    })
}

function stay(stay){
    request("game/start", "POST", "{\"privateKey\": " + privatekey + ", \"stayed\": " + stay +"}", function(text){
    })
}

function getstate(){
    request("block/height", "GET", "", function(text){
        blockHeight = text;
    })

    request("game/accountstate/" + address, "GET", "", function(text){
        const accountstate = JSON.parse(text);
        moneyEarned = accountstate.moneyEarned;
        moneyCumulated = accountstate.moneyCumulated;
        cumulatingSince = accountstate.cumulatingSince;
        randomSeed = accountstate.randomSeed;
        gamestatus = accountstate.status;
    })
}

function logstate(){
    console.log(privatekey);
    console.log(address);
    console.log(blockHeight);
    console.log(moneyEarned)
    console.log(moneyCumulated);
    console.log(cumulatingSince);
    console.log(randomSeed);
}


standby();
const syncwithchain = async () => {
    const _sleep = (delay) => new Promise((resolve) => setTimeout(resolve, delay));
    await start();
    logstate();
    await _sleep(3000);
    await getstate();
    logstate();
};
syncwithchain();

