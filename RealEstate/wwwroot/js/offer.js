const offers = document.querySelectorAll('.card');
height = 0;

offers.forEach(o => {
    if (o.offsetHeight > height)
        height = o.offsetHeight;
});

offers.forEach(o => {
    o.style.height = height + 'px';
});

