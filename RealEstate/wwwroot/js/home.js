const slides = document.querySelector('.slides');
const navButtons = document.querySelectorAll('.nav-button');
const descriptions = document.querySelectorAll('content .wrapper .desc');

let descriptionIndex = 0;
let descriptionCounter = 0
descriptions.forEach(d => {
    if (descriptionCounter % 2 === 0) {        
        d.style.marginRight = '-50%'
        d.style.marginLeft = '50%'
    } else {        
        d.style.marginRight = '50%'
        d.style.marginLeft = '-50%'
    }
    d.style.opacity = 0;
    descriptionCounter++;
});

navButtons.forEach(b => {
    b.addEventListener('click', c => {
        navButtons.forEach(btn => {
            btn.classList.remove('selected');
        })
        b.classList.add('selected');
        let counter = 0;
        navButtons.forEach(btn => {
            if (!btn.classList.contains('selected')) {
                counter++;
            } else {
                moveImages(counter);
                return;
            }
        })
    });
});

window.addEventListener('scroll', checkPosition);

function moveImages(index) {
    const image = document.querySelector('.slide-image');
    image.style.marginLeft = -(window.innerWidth * index) + 'px';
    console.log(image.style.marginLeft);
};

function checkPosition() {
    //console.log('Bottom of viewport: ', window.scrollY + window.innerHeight, 'px');
    //console.log('Description offset:', descriptions[descriptionIndex].offsetTop + 200)
    //console.log('dc: ', descriptionCounter, ' di: ', descriptionIndex)
    if (descriptionCounter <= descriptionIndex) {
        window.removeEventListener('scroll', checkPosition)
        return; 
    }

    if (descriptions[descriptionIndex].offsetTop + 100 < window.scrollY + window.innerHeight) {
        descriptions[descriptionIndex].style.marginRight = '0';
        descriptions[descriptionIndex].style.marginLeft = '0';
        descriptions[descriptionIndex].style.opacity = 1;
        ++descriptionIndex;
    }
}