export interface PhotoResponse {
  item1: string;
  item2: Boolean;
  item3: string;
  item4: MarsRoverData;
}

export interface MarsRoverData {
  photos: Photo[];
}

export interface Photo {
  id: number;
  sol: number;
  camera: Camera;
  img_Src: string;
}

export interface Camera {
  id: number;
  name: string;
  rover_Id: number;
  full_Name: string;
}
