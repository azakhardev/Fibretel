const slides = document.querySelector('.slides');
const navButtons = document.querySelectorAll('.nav-button');


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

function moveImages(index)
{
    const image = document.querySelector('.slide-image');
    image.style.marginLeft = -(window.innerWidth * index) + 'px';
    console.log(image.style.marginLeft);
};