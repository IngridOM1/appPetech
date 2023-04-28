const carouselContainer = document.querySelector('.carousel__container');
const carouselItems = document.querySelectorAll('.carousel__item');
const carouselDots = document.querySelectorAll('.carousel__dot');
const totalItems = carouselItems.length;
let slide = 0;

function moveToSlide(index) {
  carouselContainer.style.transform = `translateX(-${index * 100}%)`;
  slide = index;

  // actualiza la clase del punto activo
  for (let i = 0; i < carouselDots.length; i++) {
    if (i === index) {
      carouselDots[i].classList.add('carousel__dot--active');
    } else {
      carouselDots[i].classList.remove('carousel__dot--active');
    }
  }
}

function nextSlide() {
  if (slide === totalItems - 1) {
    moveToSlide(0);
  } else {
    moveToSlide(slide + 1);
  }
}

// establece un intervalo para cambiar de diapositiva automáticamente
setInterval(nextSlide, 5000);

// añade control de clic para los puntos
for (let i = 0; i < carouselDots.length; i++) {
  carouselDots[i].addEventListener('click', () => moveToSlide(i));
}

// mueve a la primera diapositiva al cargar la página
moveToSlide(0);
