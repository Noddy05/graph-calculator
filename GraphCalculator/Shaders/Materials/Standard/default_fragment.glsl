   
    #version 330 core

    uniform vec3 scale = vec3(1, 1, 0);
    in vec3 vPosition;
    in vec2 functionValue;
    out vec4 color;

    #libs

    void main() {
        float angle = mod(atan(functionValue.y, functionValue.x) / (2 * pi) + 0.5, 1);
        vec3 rgb = HSVToRGB(vec3(angle, 1, 1 - 1 / (4 * pow(length(functionValue), 2) + 1)));
        color = vec4(vec3(rgb.x, rgb.y, rgb.z), 1);
    }






