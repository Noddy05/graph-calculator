

vec3 HSVToRGB(vec3 hsv) {
    hsv.x *= 360;
    float C = hsv.y * hsv.z;
    float X = C * (1 - abs(mod(hsv.x / 60.0, 2) - 1));
    float m = hsv.z - C;
    vec3 rgbPrime = vec3(0);
    if(hsv.x < 60){
        rgbPrime = vec3(C, X, 0);
    } else if(hsv.x < 120){
        rgbPrime = vec3(X, C, 0);
    } else if(hsv.x < 180){
        rgbPrime = vec3(0, C, X);
    } else if(hsv.x < 240){
        rgbPrime = vec3(0, X, C);
    } else if(hsv.x < 300){
        rgbPrime = vec3(X, 0, C);
    } else {
        rgbPrime = vec3(C, 0, X);
    }
    return rgbPrime + m;
}

vec3 RGBToHSV(vec3 rgb){
    float r = rgb.x, g = rgb.y, b = rgb.z;
    float Cmax = max(r, max(g, b));
    float Cmin = min(r, min(g, b));
    float delta = Cmax - Cmin;
    float h = 0;
    if(Cmax == r){
        h = 1 / 6 * (mod((g - b) / delta, 6));
    } else if(Cmax == g){
        h = 1 / 6 * ((b - r) / delta + 2);
    } else if(Cmax == b){
        h = 1 / 6 * ((r - g) / delta + 4);
    }
    float s = 0;
    if(Cmax != 0){
        s = delta / Cmax;
    }
    float v = Cmax;

    return vec3(h, s, v);
}