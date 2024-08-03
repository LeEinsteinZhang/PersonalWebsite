import * as fs from 'fs';
import * as path from 'path';

const defaultLang = 'en';

export const loadContent = (page: string, lang: string = defaultLang): object | null => {
  const contentPath = path.join(__dirname, '../../public/content', page, `${lang}.json`);
  const basePath = path.join(__dirname, '../../public/content/base', `${lang}.json`);

  if (!fs.existsSync(contentPath) || !fs.existsSync(basePath)) {
    return null;
  }

  const content = JSON.parse(fs.readFileSync(contentPath, 'utf-8'));
  const footerContent = JSON.parse(fs.readFileSync(basePath, 'utf-8'));

  return { ...content, ...footerContent };
};
