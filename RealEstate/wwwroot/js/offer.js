const offers = document.querySelectorAll('.card');
//const filterBtn = document.querySelector('.filter-btn');
//const filterForm = document.querySelector('.filter-form');

height = 0;

offers.forEach(o => {
    if (o.offsetHeight > height)
        height = o.offsetHeight;
});

offers.forEach(o => {
    o.style.height = height + 'px';
});

//filterBtn.addEventListener('click', () => {
//    filterForm.classList.remove('invisible');
//    filterForm.classList.remove('hidden');
//    filterBtn.classList.add('hidden');
//    setTimeout(() => {
//        filterBtn.classList.add('invisible');
//    }, 500);

//});