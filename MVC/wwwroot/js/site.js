// Write your JavaScript code.

var cancel = function (e) {
    if (e.preventDefault) e.preventDefault();
    if (e.stopPropagation) e.stopPropagation();
    return false;
}

var dragOver = function (e) {
    cancel(e);
    e.dataTransfer.dropEffect = 'copy';
    this.classList.add('over');
}

var dragLeave = function (e) {
    this.classList.remove('over');
}

var dropped = function (e) {
    cancel(e);

    var target = this;
    var types = e.dataTransfer.types;

    types.forEach(function (type) {
        if (type === 'Files') {
            var files = e.dataTransfer.files;
            [].forEach.call(files, function (file) {
                p = document.createElement('p');
                p.innerHTML =
                    '<b>Type</b>: ' + file.type + '<br>' +
                '<b>Name</b>: ' + file.name + '<br />' +
                '<b>Size</b>: ' + file.size + ' kb<br />' +
                    '<b>Modified Date</b>: ' +
                    file.lastModifiedDate +
                    '<br /><hr />';
                target.appendChild(p);
            });
        }
    });
    target.classList.remove('over');
};

var target = document.querySelector('#target');
target.addEventListener('dragenter', cancel, false);
target.addEventListener('dragover', dragOver, false);
target.addEventListener('dragleave', dragLeave, false);
target.addEventListener('drop', dropped, false);

var clearButton = document.getElementById('clear');
clearButton.addEventListener('click', clearTarget, false);
