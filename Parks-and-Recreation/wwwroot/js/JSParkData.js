const queryForm = document.getElementById('query-form');
const queryInput = document.getElementById('query-input');
const parkList = document.getElementById("park-list");

queryForm.onsubmit = FetchSomeParks;


class ParkDetails {
    constructor(ParkID, Parkname, SanctuaryName, Borough, Acres, Directions, Description, HabitatType) {
        this.ParkID = ParkID;
        this.Parkname = Parkname;
        this.SanctuaryName = SanctuaryName;
        this.Borough = Borough;
        this.Acres = Acres;
        this.Directions = Directions;
        this.Description = Description;
        this.HabitatType = HabitatType;
    }
}


//////////// CREATING PARK CARDS  ////////////


//this function turns the strings from Park detail object and turns them into nodes
function createParkCardNodes(parkDetails) {
    
    var nameNode = document.createTextNode(parkDetails.Parkname);
    var habitatNode = document.createTextNode(parkDetails.HabitatType);
    var boroughNode = document.createTextNode(parkDetails.Borough);
    var acresNode = document.createTextNode(parkDetails.Acres);
    /*var directionsNode = document.createTextNode(parkDetails.Directions);*/
    var directionsNode = parkDetails.Directions;
    /*var descriptionNode = document.createTextNode(parkDetails.Description);*/
    var descriptionNode = parkDetails.Description;

    return [nameNode, habitatNode, boroughNode, acresNode,directionsNode,descriptionNode ];
}

//this function takes an array of html nodes related to a single park, 
//puts them in a styled div, and returns the div
function createParkCardDiv(parkNodes) {
    var parkDiv = document.createElement("div");

    var titleDiv = document.createElement("h1");
    var boroughDiv = document.createElement("p");
    var habitatDiv = document.createElement("p");
    var acresDiv = document.createElement("p");
    var detailDiv = document.createElement("div");

    var directionsText = document.createElement('p');
    var directionsDiv = document.createElement("p");

    var descriptionText = document.createElement('div');
    var descriptionDiv = document.createElement('div');

    var boroughLabel = document.createElement('b');
    var habitatLabel = document.createElement('b');
    var acresLabel = document.createElement('b');
    var directionsLabel = document.createElement('b');
    var descriptionLabel = document.createElement('b');


    boroughLabel.append('Borough: ');
    habitatLabel.append("Habitat: ");
    acresLabel.append("Acres: ");
    directionsLabel.append("Directions: ");
    descriptionLabel.append("Description: ");

    //boroughLabel.classList.add('font-weight-bold');
    //habitatLabel.classList.add('font-weight-bold');
    //acresLabel.classList.add('font-weight-bold');
    //directionsLabel.classList.add('font-weight-bold');
    //descriptionLabel.classList.add('font-weight-bold');


    titleDiv.appendChild(parkNodes[0]);

    habitatDiv.appendChild(habitatLabel);
    habitatDiv.appendChild(parkNodes[1]);

    boroughDiv.appendChild(boroughLabel);
    boroughDiv.appendChild(parkNodes[2]);

    acresDiv.appendChild(acresLabel);
    acresDiv.appendChild(parkNodes[3]);

    detailDiv.appendChild(habitatDiv);
    detailDiv.appendChild(boroughDiv);    
    detailDiv.appendChild(acresDiv);

    directionsText.innerHTML = parkNodes[4];
    directionsDiv.appendChild(directionsLabel);
    directionsDiv.append(directionsText);
    

    descriptionText.innerHTML = parkNodes[5];
    descriptionDiv.appendChild(descriptionLabel);
    descriptionDiv.append(descriptionText);
    




   
    //for (let index = 1; index < parkNodes.length-2; index++) {
    //    var div = document.createElement('div');
    //    div.appendChild(parkNodes[index]);
    //    detailDiv.appendChild(div);
    //}

    
    titleDiv.classList.add('display-6')
   
    detailDiv.classList.add('card-text');
    detailDiv.classList.add('col');
    directionsDiv.classList.add('card-text');
    directionsDiv.classList.add("col");
    descriptionDiv.classList.add('card-text');
    descriptionDiv.classList.add("col");

    
    parkDiv.appendChild(titleDiv)
    parkDiv.appendChild(detailDiv);
    parkDiv.appendChild(directionsDiv);
    parkDiv.appendChild(descriptionDiv);
    parkDiv.classList.add('container');
    
    return parkDiv;
}

function populateParkList(parkRecords) {
    parkList.innerHTML = "";
    parkList.classList.add('container');
    parkRecords.forEach(element => {
        var parkNodes = createParkCardNodes(element);
        var parkCard = createParkCardDiv(parkNodes);
        parkList.appendChild(parkCard);
    });
}


//////////// API, DATA VAILATION & RESULT INJECTION  ////////////

//function resetThePage() {
//    parkList.innerHTML = "";
//}

function isValid(formInput) {
    var pattern = new RegExp(/[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?]/); //unacceptable chars
    if (pattern.test(formInput)) {
        alert("Please only use standard alphanumerics");
        return false;
    }
    return true; //good user input
}

function apiCall(query) {
   
    fetch(`https://localhost:44379/ParkJSON?search=${query}`)
        .then(response => {
            const contentType = response.headers.get('content-type');
            if (!contentType || !contentType.includes('application/json')) {
                console.log('type error - no JSON')
                throw new TypeError("Oops, we haven't got JSON!");
            }
            return response.json()
        }).then(data => {
            console.log("Data Retrieved", data)
            
            var parkRecords = [];
            data.forEach(park => {
                console.log(park);               
                parkRecords.push(
                    new ParkDetails(
                        park.parkID,
                        park.parkname,
                        park.sanctuaryName,
                        park.borough,
                        park.acres,
                        park.directions,
                        park.description,
                        park.habitatType)
                );
            });

            populateParkList(parkRecords);
            
        });
}


function FetchSomeParks(event) {
    //resetThePage();
    var query = ""
    //this validates the query and makes an api call with it
    if (isValid(queryInput.value)) {
        query = queryInput.value;
        event.preventDefault();
        apiCall(query);
    }
    else {
        //if there's no query, this will get all the parks
        apiCall(query);
    }
}