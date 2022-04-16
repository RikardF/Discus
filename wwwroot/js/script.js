getWidth = (elementId) => {
    const element = document.getElementById(elementId);
    return element.offsetWidth;
}

getDistanceFromTop = (elementId) => {
    const element = document.getElementById(elementId);
    const coordinates = element.getBoundingClientRect();
    return coordinates.top;
}