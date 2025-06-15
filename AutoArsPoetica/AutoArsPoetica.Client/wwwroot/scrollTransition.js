let scrollHandler = null;
let resizeHandler = null;
let dotNetRefGlobal = null;

export function registerScrollHandler(dotNetRef) {
    dotNetRefGlobal = dotNetRef;
    scrollHandler = function () {
        dotNetRef.invokeMethodAsync('OnScrollChanged', window.scrollY);
    };
    resizeHandler = function () {
        dotNetRef.invokeMethodAsync('OnResize', getDocHeight(), window.innerHeight);
    };
    window.addEventListener('scroll', scrollHandler);
    window.addEventListener('resize', resizeHandler);
    // Fire once in case already scrolled
    scrollHandler();
    resizeHandler();
}

export function unregisterScrollHandler() {
    if (scrollHandler) {
        window.removeEventListener('scroll', scrollHandler);
        scrollHandler = null;
    }
    if (resizeHandler) {
        window.removeEventListener('resize', resizeHandler);
        resizeHandler = null;
    }
    dotNetRefGlobal = null;
}

function getDocHeight() {
    return Math.max(
        document.body.scrollHeight, document.documentElement.scrollHeight,
        document.body.offsetHeight, document.documentElement.offsetHeight,
        document.body.clientHeight, document.documentElement.clientHeight
    );
} 