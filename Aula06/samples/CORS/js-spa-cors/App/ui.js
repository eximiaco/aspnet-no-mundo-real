// Select DOM elements to work with
const callApiPartnersButton = document.getElementById('callAPIComPolitica');
const callApiPartnerByIdButton = document.getElementById('callAPISemPolitica');
const response = document.getElementById("response");
const label = document.getElementById('label');

function logMessage(s) {
    response.appendChild(document.createTextNode('\n' + s + '\n'));
}