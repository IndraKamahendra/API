// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const currentLocation = location.href;
const menuItem = document.querySelectorAll('a');
const menuLenght = menuItem.length;
for (let i = 0; i < menuLenght; i++) {
    if (menuItem[i].href === currentLocation) {
        menuItem[i].className = "active";
    } else {
        menuItem.className = "";
    }
}

document.getElementById("myBtn").addEventListener("click", displayDate);

function displayDate() {
    document.getElementById("demo").innerHTML = "Hi Selamat Datang";
}
/*document.addEventListener("click", function () {
    var i = document.querySelectorAll("home")
    for (j = 0; j < i.length; j++) {
        i[j].innerHTML = "Hello Word";
    }
});*/

/*const btn = document.getElementById("img1");
btn.addEventListener("click", () => {
    if (btn.className == "btn btn-primary btn70") {
        btn.innerHTML = `<img src=" / image / img2.jpg">`;
    }
})
*/
/*const navItems = document.querySelectorAll('.nav-item')

for (var i = 0; i < navItems.length; i++) {
  navItems[i].addEventListener("click", function() {
   
      var prev = document.getElementsByClassName("active")
 
      if (prev && prev[0]) {
          prev[0].classList.remove("active");
      }
    this.classList.add('active')
  })*/
/*let links = document.querySelectorAll('.nav-link');
for (let i = 0; i < links.length; i++) {
    links[i].addEventListener('click', function () {
        for (let j = 0; j < links.length; j++)
            links[j].classList.remove('active');
        this.classList.add('active');
    });
}*/
/*var links = document.getElementsByTagName("a"); // more specific selector if other links
for (var i = 0; i < links.length; i++) {
    var link = links[i];
    link.onclick = function () {
        var prev = document.getElementsByClassName("active");
        if (prev && prev[0]) {
            prev[0].className = ""; // if using other classes, filter better
        }
        this.className += " active";
    };
}*/
