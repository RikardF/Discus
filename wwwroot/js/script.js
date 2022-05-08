getWidth = (elementId) => {
    const element = document.getElementById(elementId);
    return element.offsetWidth;
}

getDistanceFromTop = (elementId) => {
    const element = document.getElementById(elementId);
    const coordinates = element.getBoundingClientRect();
    return coordinates.top;
}

handleDropdownMenu = (X, Y) => {
    const menu = document.getElementById("dropdown");
    const coordinates = menu.getBoundingClientRect();
    if(X > coordinates.right || X < coordinates.left || Y > coordinates.bottom || Y < coordinates.top) {
        return true;
    }
    return false;
}