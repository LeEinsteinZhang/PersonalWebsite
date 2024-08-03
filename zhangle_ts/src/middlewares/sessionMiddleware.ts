import { Request, Response, NextFunction } from 'express';
import { v4 as uuidv4 } from 'uuid';

const defaultLang = 'en';

export const ensureSession = (req: Request, res: Response, next: NextFunction): void => {
  if (!req.session.session_id) {
    req.session.session_id = uuidv4();
  }
  if (!req.session.lang) {
    req.session.lang = defaultLang;
  }
  next();
};
