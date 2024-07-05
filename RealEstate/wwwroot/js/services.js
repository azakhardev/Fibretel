const moreButton = document.querySelector('.btn-more');
const services = document.querySelectorAll('.service-card');

let counter = 0;

services.forEach(s => {
    if (counter > 5) {
        s.classList.add('noDisplay');
    }
    counter++;
});

moreButton.addEventListener('click', c => {
    services.forEach(s => {
        s.classList.remove('noDisplay');
    });
    moreButton.style.display = 'none';
})