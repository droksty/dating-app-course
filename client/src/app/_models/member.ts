import { Photo } from "./photo";

export interface Member {
  id: number;
  username: string;
  photoURL: string;
  age: number;
  knownAs: string;
  createdOn: Date;
  lastActive: Date;
  gender: string;
  introduction: string;
  lookingFor: string;
  interests: string;
  city: string;
  country: string;
  photos: Photo[];
}
