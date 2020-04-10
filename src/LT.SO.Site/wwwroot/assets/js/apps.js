/*   
Template Name: Source Admin - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.7 & Bootstrap 4
Version: 1.5.0
Author: Sean Ngu
Website: http://www.seantheme.com/source-admin-v1.5/admin/
    ----------------------------
        APPS CONTENT TABLE
    ----------------------------
    
    <!-- ======== GLOBAL SCRIPT SETTING ======== -->
    01. Handle Sidebar   - Menu
    02. Handle Sidebar   - Mobile View Toggle
    03. Handle Sidebar   - Minify / Expand
    04. Handle Sidebar   - Mobile Scrolling Feature
    05. Handle Sidebar   - Clear Sidebar Selection
    06. Handle Sidebar - Scroll Memory
	07. Handle Sidebar - Float Submenu
    08. Handle Top Menu  - Unlimited Top Menu Render
    09. Handle Top Menu  - Sub Menu Toggle
    10. Handle Top Menu  - Mobile Sub Menu Toggle
    11. Handle Top Menu  - Mobile Top Menu Toggle
    12. Handle Plugins   - Scrollbar
    13. Handle Plugins   - Bootstrap Tooltip & Popover
    14. Handle Page Load - Show Content
    15. Handle Page 	 - Scroll To Top
    16. Handle Panel     - Remove / Reload / Collapse / Expand
	17. Handle Header - Scroll Class
	18. Handle Check  - Bootstrap Version
	
    <!-- ======== APPLICATION SETTING ======== -->
    Application Controller
*/

/* 01. Handle Sidebar - Menu
------------------------------------------------ */
var handleSidebarMenu = function() {
    "use strict";
    
    $(document).on('click', '.sidebar .nav > .has-sub > a', function() {
        var target = $(this).next('.sub-menu');
        var otherMenu = '.sidebar .nav > li.has-sub > .sub-menu';

        if ($('.page-sidebar-minified').length === 0) {
            $(otherMenu).not(target).slideUp(250, function() {
                $(this).closest('li').removeClass('expand');
            });
            $(target).slideToggle(250, function() {
                var targetLi = $(this).closest('li');
                if ($(targetLi).hasClass('expand')) {
                    $(targetLi).removeClass('expand');
                } else {
                    $(targetLi).addClass('expand');
                }
            });
        }
    });
    $(document).on('click', '.sidebar .nav > .has-sub .sub-menu li.has-sub > a', function() {
        if ($('.page-sidebar-minified').length === 0) {
            var target = $(this).next('.sub-menu');
            $(target).slideToggle(250);
        }
    });
};


/* 02. Handle Sidebar - Mobile View Toggle
------------------------------------------------ */
var handleMobileSidebarToggle = function() {
    "use strict";
    var sidebarProgress = false;
    $('.sidebar').bind('click touchstart', function(e) {
        if ($(e.target).closest('.sidebar').length !== 0) {
            sidebarProgress = true;
        } else {
            sidebarProgress = false;
            e.stopPropagation();
        }
    });

    $(document).bind('click touchstart', function(e) {
        if ($(e.target).closest('.sidebar').length === 0) {
            sidebarProgress = false;
        }
        if (!e.isPropagationStopped() && sidebarProgress !== true) {
            if ($('#page-container').hasClass('page-sidebar-toggled')) {
                sidebarProgress = true;
                $('#page-container').removeClass('page-sidebar-toggled');
            }
            if ($(window).width() <= 767) {
                if ($('#page-container').hasClass('page-right-sidebar-toggled')) {
                    sidebarProgress = true;
                    $('#page-container').removeClass('page-right-sidebar-toggled');
                }
            }
        }
    });

    $(document).on('click', '[data-click="right-sidebar-toggled"]', function(e) {
        e.stopPropagation();
        var targetContainer = '#page-container';
        var targetClass = 'page-right-sidebar-toggled';
        
        if ($(targetContainer).hasClass(targetClass)) {
            $(targetContainer).removeClass(targetClass);
        } else if (sidebarProgress !== true) {
            $(targetContainer).addClass(targetClass);
        } else {
            sidebarProgress = false;
        }
    });

    $(document).on('click', '[data-click="sidebar-toggled"]', function(e) {
        e.stopPropagation();
        var sidebarClass = 'page-sidebar-toggled';
        var targetContainer = '#page-container';

        if ($(targetContainer).hasClass(sidebarClass)) {
            $(targetContainer).removeClass(sidebarClass);
        } else if (sidebarProgress !== true) {
            $(targetContainer).addClass(sidebarClass);
        } else {
            sidebarProgress = false;
        }
        if ($(window).width() < 480) {
            $('#page-container').removeClass('page-right-sidebar-toggled');
        }
    });
};


/* 03. Handle Sidebar - Minify / Expand
------------------------------------------------ */
var handleSidebarMinify = function() {
    "use strict";
    $(document).on('click', '[data-click="sidebar-minify"]', function(e) {
        e.preventDefault();
        var sidebarClass = 'page-sidebar-minified';
        var targetContainer = '#page-container';
        $('#sidebar [data-scrollbar="true"]').css('margin-top','0');
        $('#sidebar [data-scrollbar="true"]').removeAttr('data-init');
        $('#sidebar [data-scrollbar=true]').stop();
        if ($(targetContainer).hasClass(sidebarClass)) {
            $(targetContainer).removeClass(sidebarClass);
        } else {
            $(targetContainer).addClass(sidebarClass);
    
            if(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                $('#sidebar [data-scrollbar="true"]').css('margin-top','0');
                $('#sidebar [data-scrollbar="true"]').css('overflow-x', 'scroll');
            }
        }
        $(window).trigger('resize');
    });
};


/* 04. Handle Sidebar - Mobile Scrolling Feature
------------------------------------------------ */
var handleMobileSidebar = function() {
    "use strict";
    if(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        if ($('#page-container').hasClass('page-sidebar-minified')) {
            $('#sidebar [data-scrollbar="true"]').css('overflow', 'visible');
            $('.page-sidebar-minified #sidebar [data-scrollbar="true"]').slimScroll({destroy: true});
            $('.page-sidebar-minified #sidebar [data-scrollbar="true"]').removeAttr('style');
            $('.page-sidebar-minified #sidebar [data-scrollbar=true]').trigger('mouseover');
        }
    }

    var oriTouch = 0;
    $('.page-sidebar-minified .sidebar [data-scrollbar=true] a').bind('touchstart', function(e) {
        var touch = e.originalEvent.touches[0] || e.originalEvent.changedTouches[0];
        var touchVertical = touch.pageY;
        oriTouch = touchVertical - parseInt($(this).closest('[data-scrollbar=true]').css('margin-top'));
    });

    $('.page-sidebar-minified .sidebar [data-scrollbar=true] a').bind('touchmove',function(e){
        e.preventDefault();
        if(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            var touch = e.originalEvent.touches[0] || e.originalEvent.changedTouches[0];
            var touchVertical = touch.pageY;
            var elementTop = touchVertical - oriTouch;
            
            $(this).closest('[data-scrollbar=true]').css('margin-top', elementTop + 'px');
        }
    });

    $('.page-sidebar-minified .sidebar [data-scrollbar=true] a').bind('touchend', function(e) {
        var targetScrollBar = $(this).closest('[data-scrollbar=true]');
        var windowHeight = $(window).height();
        var sidebarTopPosition = parseInt($('#sidebar').css('top'));
        var sidebarContainerHeight = $('#sidebar').height();
        oriTouch = $(targetScrollBar).css('margin-top');

        var sidebarHeight = sidebarTopPosition;
        $('.sidebar').not('.sidebar-right').find('.nav').each(function() {
            sidebarHeight += $(this).height();
        });
        var finalHeight = -parseInt(oriTouch) + $('.sidebar').height();
        if (finalHeight >= sidebarHeight && windowHeight <= sidebarHeight && sidebarContainerHeight <= sidebarHeight) {
            var finalMargin = windowHeight - sidebarHeight - 20;
            $(targetScrollBar).animate({marginTop: finalMargin + 'px'});
        } else if (parseInt(oriTouch) >= 0 || sidebarContainerHeight >= sidebarHeight) {
            $(targetScrollBar).animate({marginTop: '0px'});
        } else {
            finalMargin = oriTouch;
            $(targetScrollBar).animate({marginTop: finalMargin + 'px'});
        }
        return true;
    });
};


/* 05. Handle Sidebar - Clear Sidebar Selection
------------------------------------------------ */
var handleClearSidebarSelection = function() {
    $('.sidebar .nav > li, .sidebar .nav .sub-menu').removeClass('expand').removeAttr('style');
};
var handleClearSidebarMobileSelection = function() {
    $('#page-container').removeClass('page-sidebar-toggled');
};


/* 06. Handle Sidebar - Scroll Memory
------------------------------------------------ */
var handleSidebarScrollMemory = function() {
	if (!(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))) {
		$('.sidebar [data-scrollbar="true"]').slimScroll().bind('slimscrolling', function(e, pos) {
			localStorage.setItem('sidebarScrollPosition', pos + 'px');
		});
	
		var defaultScroll = localStorage.getItem('sidebarScrollPosition');
		if (defaultScroll) {
			$('.sidebar [data-scrollbar="true"]').slimScroll({ scrollTo: defaultScroll });
		}
	}
};


/* 07. Handle Sidebar - Float Submenu
------------------------------------------------ */
var floatSubMenuTimeout;
var targetFloatMenu;
var handleMouseoverFloatSubMenu = function(elm) {
	clearTimeout(floatSubMenuTimeout);
};
var handleMouseoutFloatSubMenu = function(elm) {
	floatSubMenuTimeout = setTimeout(function() {
		$('#float-sub-menu').remove();
	}, 150);
};
var handleSidebarMinifyFloatMenu = function() {
	$(document).on('click', '#float-sub-menu li.has-sub > a', function(e) {
		var target = $(this).next('.sub-menu');
		var targetLi = $(target).closest('li');
		var close = false;
		var expand = false;
		if ($(target).is(':visible')) {
			$(targetLi).addClass('closing');
			close = true;
		} else {
			$(targetLi).addClass('expanding');
			expand = true;
		}
		$(target).slideToggle({
			duration: 250,
			progress: function() {
				var targetMenu = $('#float-sub-menu');
				var targetHeight = $(targetMenu).height();
				var targetOffset = $(targetMenu).offset();
				var targetOriTop = $(targetMenu).attr('data-offset-top');
				var targetMenuTop = $(targetMenu).attr('data-menu-offset-top');
				var targetTop 	 = targetOffset.top - $(window).scrollTop();
				var windowHeight = $(window).height();
				if (close) {
					if (targetTop > targetOriTop) {
						targetTop = (targetTop > targetOriTop) ? targetOriTop : targetTop;
						$('#float-sub-menu').css({ 'top': targetTop + 'px', 'bottom': 'auto' });
						$('#float-sub-menu-arrow').css({ 'top': '20px', 'bottom': 'auto' });
						$('#float-sub-menu-line').css({ 'top': '20px', 'bottom': 'auto' });
					}
				}
				if (expand) {
					if ((windowHeight - targetTop) < targetHeight) {
						var arrowBottom = (windowHeight - targetMenuTop) - 22;
						$('#float-sub-menu').css({ 'top': 'auto', 'bottom': 0 });
						$('#float-sub-menu-arrow').css({ 'top': 'auto', 'bottom': arrowBottom + 'px' });
						$('#float-sub-menu-line').css({ 'top': '20px', 'bottom': arrowBottom + 'px' });
					}
				}
			},
			complete: function() {
				if ($(target).is(':visible')) {
					$(targetLi).addClass('expand');
				} else {
					$(targetLi).addClass('closed');
				}
				$(targetLi).removeClass('closing expanding');
			}
		});
	});
	$('.sidebar .nav > li.has-sub > a').hover(function() {
		if ($('#page-container').hasClass('page-sidebar-minified')) {
			clearTimeout(floatSubMenuTimeout);

			var targetMenu = $(this).closest('li').find('.sub-menu').first();
			if (targetFloatMenu == this && $('#float-sub-menu').length !== 0) {
				return;
			} else {
				targetFloatMenu = this;
			}
			var targetMenuHtml = $(targetMenu).html();
			if (targetMenuHtml) {
				var sidebarOffset = $('#sidebar').offset();
				var sidebarX = (!$('#page-container').hasClass('page-with-right-sidebar')) ? (sidebarOffset.left + 70) : ($(window).width() - sidebarOffset.left);
				var targetHeight = $(targetMenu).height();
				var targetOffset = $(this).offset();
				var targetTop = targetOffset.top - $(window).scrollTop();
				var targetLeft = (!$('#page-container').hasClass('page-with-right-sidebar')) ? sidebarX : 'auto';
				var targetRight = (!$('#page-container').hasClass('page-with-right-sidebar')) ? 'auto' : sidebarX;
				var windowHeight = $(window).height();
	
				if ($('#float-sub-menu').length === 0) {
					targetMenuHtml = ''+
					'<div class="float-sub-menu-container" id="float-sub-menu" data-offset-top="'+ targetTop +'" data-menu-offset-top="'+ targetTop +'" onmouseover="handleMouseoverFloatSubMenu(this)" onmouseout="handleMouseoutFloatSubMenu(this)">'+
					'	<ul class="float-sub-menu">'+ targetMenuHtml + '</ul>'+
					'</div>';
					$('#page-container').append(targetMenuHtml);
				} else {
					$('#float-sub-menu').attr('data-offset-top', targetTop);
					$('#float-sub-menu').attr('data-menu-offset-top', targetTop);
					$('.float-sub-menu').html(targetMenuHtml);
				}
				
				if ((windowHeight - targetTop) > targetHeight) {
					$('#float-sub-menu').css({
						'top': targetTop,
						'left': targetLeft,
						'bottom': 'auto',
						'right': targetRight
					});
				} else {
					$('#float-sub-menu').css({
						'bottom': 0,
						'top': 'auto',
						'left': targetLeft,
						'right': targetRight
					});
				}
			} else {
				$('#float-sub-menu').remove();
				targetFloatMenu = '';
			}
		}
	}, function() {
		if ($('#page-container').hasClass('page-sidebar-minified')) {
			floatSubMenuTimeout = setTimeout(function() {
				$('#float-sub-menu').remove();
				targetFloatMenu = '';
			}, 250);
		}
	});
};


/* 08. Handle Top Menu - Unlimited Top Menu Render
------------------------------------------------ */
var handleUnlimitedTopMenuRender = function() {
    "use strict";
    // function handle menu button action - next / prev
    function handleMenuButtonAction(element, direction) {
        var obj = $(element).closest('.nav');
        var marginLeft = parseInt($(obj).css('margin-left'));  
        var containerWidth = $('.top-menu').width() - 88;
        var totalWidth = 0;
        var finalScrollWidth = 0;

        $(obj).find('li').each(function() {
            if (!$(this).hasClass('menu-control')) {
                totalWidth += $(this).width();
            }
        });
        
        switch (direction) {
            case 'next':
                var widthLeft = totalWidth + marginLeft - containerWidth;
                if (widthLeft <= containerWidth) {
                    finalScrollWidth = widthLeft - marginLeft + 128;
                    setTimeout(function() {
                        $(obj).find('.menu-control.menu-control-right').removeClass('show');
                    }, 150);
                } else {
                    finalScrollWidth = containerWidth - marginLeft - 128;
                }

                if (finalScrollWidth != 0) {
                    $(obj).animate({ marginLeft: '-' + finalScrollWidth + 'px'}, 150, function() {
                        $(obj).find('.menu-control.menu-control-left').addClass('show');
                    });
                }
                break;
            case 'prev':
                var widthLeft = -marginLeft;
    
                if (widthLeft <= containerWidth) {
                    $(obj).find('.menu-control.menu-control-left').removeClass('show');
                    finalScrollWidth = 0;
                } else {
                    finalScrollWidth = widthLeft - containerWidth + 88;
                }
                $(obj).animate({ marginLeft: '-' + finalScrollWidth + 'px'}, 150, function() {
                    $(obj).find('.menu-control.menu-control-right').addClass('show');
                });
                break;
        }
    }

    // handle page load active menu focus
    function handlePageLoadMenuFocus() {
        var targetMenu = $('.top-menu .nav');
        var targetList = $('.top-menu .nav > li');
        var targetActiveList = $('.top-menu .nav > li.active');
        var targetContainer = $('.top-menu');
        
        var marginLeft = parseInt($(targetMenu).css('margin-left'));  
        var viewWidth = $(targetContainer).width() - 128;
        var prevWidth = $('.top-menu .nav > li.active').width();
        var speed = 0;
        var fullWidth = 0;
        
        $(targetActiveList).prevAll().each(function() {
            prevWidth += $(this).width();
        });

        $(targetList).each(function() {
            if (!$(this).hasClass('menu-control')) {
                fullWidth += $(this).width();
            }
        });

        if (prevWidth >= viewWidth) {
            var finalScrollWidth = prevWidth - viewWidth + 128;
            $(targetMenu).animate({ marginLeft: '-' + finalScrollWidth + 'px'}, speed);
        }
        
        if (prevWidth != fullWidth && fullWidth >= viewWidth) {
            $(targetMenu).find('.menu-control.menu-control-right').addClass('show');
        } else {
            $(targetMenu).find('.menu-control.menu-control-right').removeClass('show');
        }

        if (prevWidth >= viewWidth && fullWidth >= viewWidth) {
            $(targetMenu).find('.menu-control.menu-control-left').addClass('show');
        } else {
            $(targetMenu).find('.menu-control.menu-control-left').removeClass('show');
        }
    }

    // handle menu next button click action
    $(document).on('click', '[data-click="next-menu"]', function(e) {
        e.preventDefault();
        handleMenuButtonAction(this,'next');
    });

    // handle menu prev button click action
    $(document).on('click', '[data-click="prev-menu"]', function(e) {
        e.preventDefault();
        handleMenuButtonAction(this,'prev');

    });

    // handle unlimited menu responsive setting
    $(window).resize(function() {
        $('.top-menu .nav').removeAttr('style');
        handlePageLoadMenuFocus();
    });

    handlePageLoadMenuFocus();
};


/* 09. Handle Top Menu - Sub Menu Toggle
------------------------------------------------ */
var handleTopMenuSubMenu = function() {
    "use strict";
    $('.top-menu .sub-menu .has-sub > a').click(function() {
        var target = $(this).closest('li').find('.sub-menu').first();
        var otherMenu = $(this).closest('ul').find('.sub-menu').not(target);
        $(otherMenu).not(target).slideUp(250, function() {
            $(this).closest('li').removeClass('expand');
        });
        $(target).slideToggle(250, function() {
            var targetLi = $(this).closest('li');
            if ($(targetLi).hasClass('expand')) {
                $(targetLi).removeClass('expand');
            } else {
                $(targetLi).addClass('expand');
            }
        });
    });
};


/* 10. Handle Top Menu - Mobile Sub Menu Toggle
------------------------------------------------ */
var handleMobileTopMenuSubMenu = function() {
    "use strict";
    $(document).on('click', '.top-menu .nav > li.has-sub > a', function() {
        if ($(window).width() <= 767) {
            var target = $(this).closest('li').find('.sub-menu').first();
            var otherMenu = $(this).closest('ul').find('.sub-menu').not(target);
            $(otherMenu).not(target).slideUp(250, function() {
                $(this).closest('li').removeClass('expand');
            });
            $(target).slideToggle(250, function() {
                var targetLi = $(this).closest('li');
                if ($(targetLi).hasClass('expand')) {
                    $(targetLi).removeClass('expand');
                } else {
                    $(targetLi).addClass('expand');
                }
            });
        }
    });
};


/* 11. Handle Top Menu - Mobile Top Menu Toggle
------------------------------------------------ */
var handleTopMenuMobileToggle = function() {
    "use strict";
    $(document).on('click', '[data-click="top-menu-toggled"]', function() {
        $('.top-menu').slideToggle(250);
    });
};


/* 12. Handle Plugins - Scrollbar
------------------------------------------------ */
var handleSlimScroll = function() {
    "use strict";
    $('[data-scrollbar="true"]').each(function() {
        generateSlimScroll($(this));
    });
};
var generateSlimScroll = function(element) {
    "use strict";
    if ($(element).attr('data-init')) {
        return;
    }
    var dataHeight = $(element).attr('data-height');
        dataHeight = (!dataHeight) ? $(element).height() : dataHeight;
    var dataDistance = $(element).attr('data-distance');
        dataDistance = (!dataDistance) ? '0px' : dataDistance;
    var dataPosition = $(element).attr('data-position');
        dataPosition = (!dataPosition) ? 'right' : dataPosition;

    var scrollBarOption = {
        height: dataHeight, 
        alwaysVisible: false,
        distance: dataDistance,
        position: dataPosition
    };
    if(/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        $(element).css('height', dataHeight);
        $(element).css('overflow-x','scroll');
    } else {
        $(element).slimScroll(scrollBarOption);
    }
    $(element).attr('data-init', true);
};


/* 13. Handle Plugins - Bootstrap Tooltip & Popover
------------------------------------------------ */
var handleTooltipInit = function() {
    "use strict";
    if ($('[data-toggle="tooltip"]').length !== 0) {
        $('[data-toggle="tooltip"]').tooltip();
    }
};
var handlePopoverInit = function() {
    if ($('[data-toggle="popover"]').length !== 0) {
        $('[data-toggle="popover"]').popover();
    }
};


/* 14. Handle Page Load - Show Content
------------------------------------------------ */
var handlePageLoad = function() {
    "use strict";
    $('#page-loader').addClass('hide');
    if (handleCheckBootstrapVersion() < 4) {
   		$('#page-container').addClass('in');
    } else {
   		$('#page-container').addClass('show');
    }
};


/* 15. Handle Page -Scroll To Top
------------------------------------------------ */
var handleScrollToTopButton = function() {
    "use strict";
    $(document).on('click', '[data-click=scroll-top]', function(e) {
        e.preventDefault();
        $('html, body').animate({
            scrollTop: $('body').offset().top
        }, 500);
    });
};


/* 16. Handle Panel - Remove / Reload / Collapse / Expand
------------------------------------------------ */
var panelActionRunning = false;
var handlePanelAction = function() {
    "use strict";
    
    if (panelActionRunning) {
        return false;
    }
    panelActionRunning = true;
    
    // remove
    $('[data-click="panel-remove"]').hover(function() {
        $(this).tooltip({
            title: 'Remove',
            placement: 'bottom',
            trigger: 'hover',
            container: 'body'
        });
        $(this).tooltip('show');
    });
    $(document).on('click', '[data-click="panel-remove"]', function(e) {
        e.preventDefault();
        var bootstrapVersion = handleCheckBootstrapVersion();
        
        if (bootstrapVersion >= 4 && bootstrapVersion < 5) {
        	$(this).tooltip('dispose');
        } else {
        	$(this).tooltip('destroy');
        }
        $(this).closest('.panel').remove();
    });
    
    // collapse
    $('[data-click="panel-collapse"]').hover(function() {
        $(this).tooltip({
            title: 'Collapse / Expand',
            placement: 'bottom',
            trigger: 'hover',
            container: 'body'
        });
        $(this).tooltip('show');
    });
    $(document).on('click', '[data-click="panel-collapse"]', function(e) {
        e.preventDefault();
        $(this).closest('.panel').find('.panel-body').slideToggle();
    });
    
    // reload
    $('[data-click="panel-reload"]').hover(function() {
        $(this).tooltip({
            title: 'Reload',
            placement: 'bottom',
            trigger: 'hover',
            container: 'body'
        });
        $(this).tooltip('show');
    });
    $(document).on('click', '[data-click="panel-reload"]', function(e) {
        e.preventDefault();
        var target = $(this).closest('.panel');
        if (!$(target).hasClass('panel-loading')) {
            var targetBody = $(target).find('.panel-body');
            var spinnerHtml = '<div class="panel-loader"><span class="spinner-small">Loading...</span></div>';
            $(target).addClass('panel-loading');
            $(targetBody).prepend(spinnerHtml);
            setTimeout(function() {
                $(target).removeClass('panel-loading');
                $(target).find('.panel-loader').remove();
            }, 2000);
        }
    });
    
    // expand
    $('[data-click="panel-expand"]').hover(function() {
        $(this).tooltip({
            title: 'Expand / Compress',
            placement: 'bottom',
            trigger: 'hover',
            container: 'body'
        });
        $(this).tooltip('show');
    });
    $(document).on('click', '[data-click="panel-expand"]', function(e) {
        e.preventDefault();
        var target = $(this).closest('.panel');
        var targetClass = 'panel-expand';
        var targetBody = $(target).find('.panel-body');
        var callbackFunction = ($(target).hasClass(targetClass)) ? $(this).attr('data-collapse-callback') : $(this).attr('data-expand-callback');
        
        if (callbackFunction) {
            window[callbackFunction]($(this));
        }
        
        if ($(target).hasClass(targetClass)) {
            $(targetBody).removeAttr('style');
            $(targetBody).find('[data-scrollbar="true"]').each(function() {
                var dataHeight = $(this).attr('data-ori-height');
                $(this).slimScroll({destroy: true});
                $(this).removeAttr('style');
                $(this).attr('data-height', dataHeight);
                generateSlimScroll(this);
            });
            $(target).removeClass(targetClass);
        } else {
            var finalHeight = $(window).height() - $(target).find('.panel-heading').height() - 30;
            $(targetBody).find('[data-scrollbar="true"]').each(function() {
                var dataHeight = $(this).attr('data-height');
                $(this).slimScroll({destroy: true});
                $(this).removeAttr('style');
                $(this).attr('data-ori-height', dataHeight);
                $(this).attr('data-height','100%');
                generateSlimScroll(this);
            });
            $(targetBody).css('height', finalHeight);
            $(target).addClass(targetClass);
        }
        
        $(window).trigger('resize');
    });
};


/* 17. Handle Header - Scroll Class
------------------------------------------------ */
var handleHeaderScroll = function() {
	$(window).on('scroll', function() {
		if ($(window).scrollTop() > 0) {
			$('#header').addClass('has-scroll');
		} else {
			$('#header').removeClass('has-scroll');
		}
	});
};


/* 18. Handle Check - Bootstrap Version
------------------------------------------------ */
var handleCheckBootstrapVersion = function() {
	return parseInt($.fn.tooltip.Constructor.VERSION);
};


/* Application Controller
------------------------------------------------ */
var App = function () {
	"use strict";
	
	var setting;
	
	return {
		initSidebar: function() {
		    handleSidebarMenu();
		    handleMobileSidebarToggle();
		    handleSidebarMinify();
		    handleSidebarMinifyFloatMenu();
		    handleMobileSidebar();
		    
			if (!setting || (setting && !setting.disableSidebarScrollMemory)) {
				handleSidebarScrollMemory();
			}
		},
		initSidebarSelection: function() {
		    handleClearSidebarSelection();
		},
		initSidebarMobileSelection: function() {
		    handleClearSidebarMobileSelection();
		},
		initTopMenu: function() {
		    handleUnlimitedTopMenuRender();
		    handleTopMenuSubMenu();
		    handleMobileTopMenuSubMenu();
		    handleTopMenuMobileToggle();
		},
		initHeader: function() {
		    handleHeaderScroll();
		},
		initComponent: function() {
		    handleTooltipInit();
		    handlePopoverInit();
		    handlePanelAction();
		    handleSlimScroll();
		    handleScrollToTopButton();
		},
		initPageLoad: function() {
		    handlePageLoad();
		},
		init: function (option) {
			if (option) {
				setting = option;
			}
		    this.initHeader();
		    this.initTopMenu();
		    this.initSidebar();
		    this.initComponent();
		    this.initPageLoad();
		},
		scrollTop: function() {
            $('html, body').animate({
                scrollTop: $('body').offset().top
            }, 0);
		}
  };
}();