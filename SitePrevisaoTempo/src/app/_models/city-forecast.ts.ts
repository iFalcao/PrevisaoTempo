export interface CityForecast {
    message: string;
    cod: string;
    cnt: number;
    list: Forecast[];
}

export interface Forecast {
    dt: number;
    main: Temperature;
    wind: Wind;
    weather: Weather[];
}

export interface Temperature {
    temp: number;
    pressure: number;
    humidity: number;
    temp_min: number;
    temp_max: number;
    sea_level: number;
    temp_kf: number;
}

export interface Wind {
    speed: number;
    deg: number;
}

export interface Weather {
    id: number;
    main: string;
    description: string;
    icon: string;
}