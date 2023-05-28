// Obtener elementos del carrusel
const carouselContainer = document.querySelector('.carousel__container');
const carouselItems = document.querySelectorAll('.carousel__item');
const carouselDots = document.querySelector('.carousel__dots');

// Añadir eventos a los puntos del carrusel
carouselDots.addEventListener('click', (event) => {
  if (event.target.classList.contains('carousel__dot')) {
    const targetSlide = event.target.getAttribute('data-slide');
    showSlide(targetSlide);
  }
});

// Mostrar el slide seleccionado
function showSlide(slideIndex) {
  const slideWidth = carouselItems[0].offsetWidth;
  const translateX = slideWidth * slideIndex * -1;
  carouselContainer.style.transform = `translateX(${translateX}px)`;

  // Marcar el punto activo
  carouselDots.querySelectorAll('.carousel__dot').forEach((dot) => {
    dot.classList.remove('carousel__dot--active');
  });
  carouselDots.querySelector(`[data-slide="${slideIndex}"]`).classList.add('carousel__dot--active');
}

// Iniciar carrusel
function startCarousel() {
  let slideIndex = 0;
  const totalSlides = carouselItems.length;

  setInterval(() => {
    slideIndex = (slideIndex + 1) % totalSlides;
    showSlide(slideIndex);
  }, 2000);
}

// Iniciar el carrusel cuando el contenido esté cargado
window.addEventListener('DOMContentLoaded', startCarousel);
