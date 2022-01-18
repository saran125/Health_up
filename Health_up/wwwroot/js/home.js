(function () {
"use strict";

	//===== Preloader
	/*
	window.onload = function () {
		window.setTimeout(fadeout, 500);
	}

	function fadeout() {
		document.querySelector('.preloader').style.opacity = '0';
		document.querySelector('.preloader').style.display = 'none';
	}

	*/

	/*=====================================
	Sticky
	======================================= */
	window.onscroll = function () {
		var header_navbar = document.querySelector(".navbar-area");
		var sticky = header_navbar.offsetTop;

		if (window.pageYOffset > sticky) {
			header_navbar.classList.add("sticky");
		} else {
			header_navbar.classList.remove("sticky");
		}



		// show or hide the back-top-top button
		var backToTo = document.querySelector(".scroll-top");
		if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
			backToTo.style.display = "block";
		} else {
			backToTo.style.display = "none";
		}
	};

	// Get the navbar

	//======= tiny slider for slider-active
	tns({
		container: '.slider-active',
		items: 1,
		slideBy: 'page',
		autoplay: true,
		mouseDrag: true,
		gutter: 0,
		nav: true,
		controls: false,
		autoplayButtonOutput: false,
	});

	// for menu scroll 
	var pageLink = document.querySelectorAll('.page-scroll');

	
	

	// section menu active
	function onScroll(event) {
		var sections = document.querySelectorAll('.page-scroll');
		var scrollPos = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop;

		
	};

	window.document.addEventListener('scroll', onScroll);


	//===== close navbar-collapse when a  clicked
	let navbarToggler = document.querySelector(".navbar-toggler");
	var navbarCollapse = document.querySelector(".navbar-collapse");

	
	navbarToggler.addEventListener('click', function () {
		navbarToggler.classList.toggle("active");
	})




	//======== WOW active
	new WOW().init();




})();	