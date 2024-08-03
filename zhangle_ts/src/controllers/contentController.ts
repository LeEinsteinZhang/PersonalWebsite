import { Request, Response } from 'express';
import { loadContent } from '../utils/loadContent';

export const home = (req: Request, res: Response): void => {
  const sess: any = req.session;
  const lang = sess.data.lang;
  const content = loadContent('index', lang);
  sess.data.prev_url = '/';
  res.render('index', { content, lang, current_page: 'index', default_lang: 'en', request: req });
};

export const experience = (req: Request, res: Response): void => {
  const sess: any = req.session;
  const lang = sess.data.lang;
  const experience = req.params.experience;
  const content = loadContent(`experiences/${experience}`, lang);
  if (!content) {
    res.status(404).render('404', { lang, prev_url: sess.data.prev_url || '/', request: req });
  } else {
    sess.data.prev_url = req.path;
    res.render('experience', { content, lang, current_page: experience, default_lang: 'en', request: req });
  }
};

export const switchLang = (req: Request, res: Response): void => {
  const sess: any = req.session;
  sess.data.lang = req.params.lang;
  const nextUrl = req.query.next || req.header('Referer') || '/';
  res.redirect(nextUrl.toString());
};
