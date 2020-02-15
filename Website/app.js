'use strict';

let xhr = new XMLHttpRequest();

xhr.open('GET', '/getButtons');

xhr.send();

xhr.onload = function() {
    if (xhr.status != 200) { 
        alert(`Ошибка ${xhr.status}: ${xhr.statusText}`);
    } else {
        // alert(`Готово, получили ${xhr.response.length} байт`); 
        let data = JSON.parse(xhr.response);
        data.forEach(element => {
            let div = document.createElement('div');
            div.classList.add('button');

            if(element.top !== null) div.style.top = element.top;
            if(element.left !== null) div.style.left = element.left;
            if(element.right !== null) div.style.right = element.right;
            if(element.width !== null) div.style.width = element.width;
            if(element.height !== null) div.style.height = element.height;

            if(element.backgroundColor !== null) div.style.backgroundColor = element.backgroundColor;
            if(element.color !== null) div.style.color = element.color;
            if(element.fontSize !== null) div.style.fontSize = element.fontSize;
            if(element.position !== null) div.style.position = element.position;
            if(element.lineHeight !== null) div.style.lineHeight = element.lineHeight;
            
            if(element.text !== null) div.innerHTML = element.text;

            div.KEY = element.key;
            div.onclick = (e) => {
                let xhr = new XMLHttpRequest();
                xhr.open('GET', '/sendBtn/'+e.target.KEY);
                xhr.send();
                console.log('Sent btn: '+e.target.KEY);
            };

            document.body.appendChild(div);
        });
    }
};
